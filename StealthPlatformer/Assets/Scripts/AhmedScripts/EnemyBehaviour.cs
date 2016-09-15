using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {
	public Rigidbody2D m_rigidbody2d; //Enemy rigidbody
	public enum state // Defining the enemy states
	{
		Patrolling,
		Alert,
		Switching,
		Persuing
	};
	public float m_Speed;
	public Transform[] patrolPoints;
	public state enemyState; // The current state of the enemy
	public LayerMask detectionMask;
	public float detectionRadius;
	public float detectionLevel;
	public float detectionTime;
	public Image detectionImage;
	public bool canShoot;
	public float fireRate;
	public float maxDist;
	public GameObject bullet;
	public Transform firePoint;
	public float randomX;
	public float randomY;

	Transform enemyCanvas;
	float timeToShoot;
	bool playerDetected;
	Stats teleportersScript;
	GameObject target;
	float detectionVelocity;
	public int direction = 1;
	public int level = 0;
	int playerLevel;
	void Start () {
		enemyCanvas = transform.FindChild ("Canvas");
		target = GameObject.FindGameObjectWithTag ("Player");
		m_rigidbody2d = GetComponent<Rigidbody2D> ();
		detectionImage = transform.FindChild("Canvas").transform.FindChild ("detectionBar").GetComponent<Image>();
		teleportersScript = GameObject.FindGameObjectWithTag ("Gm").GetComponent<Stats> ();
		firePoint = transform.FindChild ("FirePoint");
	}

	void Update () {
		if (direction != 0 && direction == -1 && transform.localScale.x > 0) {
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 1);
		} else if (direction == 1) {
			transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x),transform.localScale.y, 1);
		}

		playerLevel = GameObject.FindGameObjectWithTag ("Player").GetComponent<dummyPlayerScript> ().level;
		if (playerDetected == false && detectionLevel > 0 && detectionLevel < 100 && teleportersScript.detected == false) {
			Detect (-1);
		} else if (playerDetected == true && detectionLevel < 100 && teleportersScript.detected == false) {
			Detect (101);
		} 
		if (teleportersScript.detected == true) {
			Detect (101);
		}

		if (enemyState == state.Patrolling) {
			Patrol ();
		} else if (enemyState == state.Alert) {
			direction = 0;
			if (detectionLevel == 0) {
				enemyState = state.Patrolling;
				direction = 1;
			} else if (detectionLevel == 100) {
				enemyState = state.Persuing;
				teleportersScript.detected = true;
			}
		} else if (enemyState == state.Persuing) {
			Pursue ();
		} else if (enemyState == state.Switching) {
			SwitchLevel ();
		}

		m_rigidbody2d.velocity = Vector2.right * direction * m_Speed;
	}

	void Patrol () {
		if (transform.position.x >= patrolPoints [0].position.x) {
			direction = -1;
		} else if (transform.position.x <= patrolPoints[1].position.x && transform.position.x < patrolPoints[0].position.x) {
			direction = 1;
		}
	}

	void Detect (int target) {
		detectionLevel = Mathf.SmoothDamp (detectionLevel, target, ref detectionVelocity, detectionTime);
		detectionLevel = Mathf.Clamp (detectionLevel, 0, 100);
		detectionImage.fillAmount = detectionLevel / 100;

		if (detectionLevel < 100) {
			enemyState = state.Alert;
		} else if (detectionLevel > 100) {
			enemyState = state.Persuing;
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other != null && other.gameObject.tag == "Player") {
			target = other.gameObject;
			playerDetected = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other != null && other.gameObject.tag == "Player") {
			playerDetected = false;
		}
	}

	void Pursue () {
		if (playerLevel != level) {
			enemyState = state.Switching;
		} else {
			if (transform.position.x > target.transform.position.x) {
				direction = -1;
			} else {
				direction = 1;
			}
		}

		if (canShoot == true && level == playerLevel) {
			Shoot ();
		}
	}

	void Shoot () {
		GameObject shot = Instantiate (bullet, firePoint.position, transform.rotation) as GameObject;
		shot.transform.LookAt (new Vector2 (target.transform.position.x + Random.Range(-randomX, randomX), Random.Range(-randomY, randomY)));
		if (Time.time > timeToShoot && Vector2.Distance(transform.position, target.transform.position) < maxDist) {
			timeToShoot += Time.time + 10 / fireRate;
			print ("Test");
			//GameObject shot = Instantiate (bullet, firePoint.position, transform.rotation) as GameObject;
			//shot.transform.LookAt (new Vector2 (target.transform.position.x + Random.Range(-randomX, randomX), Random.Range(-randomY, randomY)));
		}
	}

	void SwitchLevel () {
		float TeleporterDistance1 = Vector2.Distance(transform.position, teleportersScript.levelArray[playerLevel].teleporter1.position);
		float TeleporterDistance2 = Vector2.Distance(transform.position, teleportersScript.levelArray[playerLevel].teleporter2.position);
		Transform targetTeleporter;

		if (playerLevel != level) {
			if (TeleporterDistance1 < TeleporterDistance2) {
				targetTeleporter = teleportersScript.levelArray [playerLevel].teleporter1;
			} else {
				targetTeleporter = teleportersScript.levelArray [playerLevel].teleporter2;
			}

			if (transform.position.x >= targetTeleporter.position.x) {
				direction = -1;
			} else if (transform.position.x <= targetTeleporter.position.x) { 
				direction = 1;
			}
		}
	}



	public void Teleport() {
		if (enemyState == state.Switching) {
			if (playerLevel > level) {
				transform.position = new Vector2 (transform.position.x, teleportersScript.levelArray [level + 1].teleporter1.transform.position.y);
				level++;
			} else if (playerLevel < level) {
				transform.position = new Vector2 (transform.position.x, teleportersScript.levelArray [level - 1].teleporter1.transform.position.y);
				level--;
			} else {
				enemyState = state.Persuing;
			}
		}
	}
}