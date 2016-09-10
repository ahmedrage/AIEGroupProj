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

	Teleporters teleportersScript;
	GameObject target;
	float detectionVelocity;
	int direction = 1;
	public int level = 0;
	int playerLevel;
	void Start () {
		m_rigidbody2d = GetComponent<Rigidbody2D> ();
		detectionImage = transform.FindChild("Canvas").transform.FindChild ("detectionBar").GetComponent<Image>();
		teleportersScript = GameObject.FindGameObjectWithTag ("Gm").GetComponent<Teleporters> ();
	}
	
	void Update () {
		playerLevel = GameObject.Find ("Player").GetComponent<dummyPlayerScript> ().level;
		Detect ();
		if (enemyState == state.Patrolling) {
			Patrol ();
		} else if (enemyState == state.Alert) {
			direction = 0;
			if (detectionLevel == 0) {
				enemyState = state.Patrolling;
				direction = 1;
			} else if (detectionLevel == 100) {
				enemyState = state.Persuing;
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
		} else if (transform.position.x <= patrolPoints[1].position.x) {
			direction = 1;
		}
	}

	void Detect () {
		RaycastHit2D hit = Physics2D.CircleCast (transform.position, detectionRadius, Vector2.zero, Mathf.Infinity, detectionMask);

		if (hit.collider != null && hit.collider.tag == "Player") {
			target = hit.collider.gameObject;
			detectionLevel = Mathf.SmoothDamp (detectionLevel, 101, ref detectionVelocity, detectionTime);

			if (detectionLevel < 100) {
				enemyState = state.Alert;
			}
		} else if ((hit.collider == null || hit.collider.tag != "Player") && detectionLevel > 0 && detectionLevel != 100) {
			detectionLevel = Mathf.SmoothDamp (detectionLevel, -1, ref detectionVelocity, detectionTime);
		}

		detectionLevel = Mathf.Clamp (detectionLevel, 0, 100);

		detectionImage.fillAmount = detectionLevel / 100;
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
	}

	void SwitchLevel () {
		float TeleporterDistance1 = Vector2.Distance(transform.position, teleportersScript.levelArray[playerLevel].teleporter1.position);
		float TeleporterDistance2 = Vector2.Distance(transform.position, teleportersScript.levelArray[playerLevel].teleporter1.position);
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

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Teleporter" && enemyState == state.Switching) {
			Teleport ();
		}
        else if (coll.gameObject.tag == "Bullet") {
            Destroy(gameObject);
        }
	}

	void Teleport() {
		if (playerLevel > level) {
			transform.position = new Vector2 (transform.position.x, teleportersScript.levelArray [level+ 1].yPos);
			level++;
		} else if (playerLevel < level) {
			transform.position = new Vector2 (transform.position.x, teleportersScript.levelArray [level- 1].yPos);
			level--;
		} else {
			enemyState = state.Persuing;
		}
	}
}