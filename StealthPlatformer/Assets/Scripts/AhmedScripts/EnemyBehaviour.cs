using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {
	public Rigidbody2D m_rigidbody2d; //Enemy rigidbody
	public enum state // Defining the enemy states
	{
		Patrolling, // The patrolling state
		Alert,      // The state in which the enemy is being alerted
		Switching,  // The state in which the enemy is changing level
		Persuing    // The state in which the enemy is chasing the player
	};
	public float m_Speed; // The movement speed
	public Transform[] patrolPoints; // The two points the enemy patrols between
	public state enemyState; // The current state of the enemy
	public float detectionLevel; // The % value of how close the enemy is to fully detecting the player
	public float detectionTime; // The ammount of seconds in detection radius for the enemy to start persuing player
	public Image detectionImage; // The detection bar
	public bool shootingEnabled; // If the enemy can ever shoot
	public bool lungingEnabled; //If the enemy can ever lunge
	public float lungeDistance;
	public float lungeMultiplier;
	public float fireDelay; // The delay between shots
	public float maxDist; // The maxiumum distance possible for the enemy to shoot
	public GameObject bullet; // The bullet
	public Transform firePoint; //THe point where the bullet is instantiated
	public float randomX; //
	public float randomY;
	public float shootFreezeTime;
	public AudioSource alertSound;
	public GameObject gun;
	public Sprite shootSprite;
	public float shootAnimTime;

	float shootAnimTimeLeft;
	Sprite initialGunSprite;
	public bool isShooting = false;
	bool playingAlert;
	bool canShoot = true; // If the enemy can currently shoot
	float initialSpeed;
	Transform enemyCanvas;
	public float timeToShoot;
	bool playerDetected;
	Stats teleportersScript;
	GameObject target;
	float detectionVelocity;
	public int direction = 1;
	public int level = 0;
	int playerLevel;
	float distanceFromPlayer;

	void Start () {
		Physics2D.IgnoreLayerCollision (11, 11);
		Physics2D.IgnoreLayerCollision (12, 12);
		Physics2D.IgnoreLayerCollision (10, 11);
		initialSpeed = m_Speed;
		enemyCanvas = transform.FindChild ("Canvas");
		target = GameObject.FindGameObjectWithTag ("Player");
		m_rigidbody2d = GetComponent<Rigidbody2D> ();
		detectionImage = transform.FindChild("Canvas").transform.FindChild ("detectionBar").GetComponent<Image>();
		teleportersScript = GameObject.FindGameObjectWithTag ("Gm").GetComponent<Stats> ();
		initialGunSprite = gun.GetComponent<SpriteRenderer> ().sprite;
		//firePoint = transform.FindChild ("FirePoint");
	}

	void Update () {
		if (Time.time > shootAnimTimeLeft) {
			gun.GetComponent<SpriteRenderer> ().sprite = initialGunSprite;
		} else {
			gun.GetComponent<SpriteRenderer> ().sprite = shootSprite;
		}

		level = teleportersScript.checkLevel (transform.position.y);
		if (target != null) {
			distanceFromPlayer = Vector2.Distance (transform.position, target.transform.position);

			if (direction != 0 && direction == -1 && transform.localScale.x > 0) {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 1);
			} else if (direction == 1) {
				transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, 1);
			}

			playerLevel = teleportersScript.playerLevel;
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
				if (!alertSound.isPlaying) {
					alertSound.Play ();
				}
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
			//isShooting = false;
			if (enemyState == state.Persuing) {
				gameObject.layer = 12;
			}
				m_rigidbody2d.velocity = Vector2.right * direction * m_Speed;
		} else {
			print ("Player not found");
		}
	}

	void Patrol () { // Runs when the enemy is in the patrolling state
		if (transform.position.x >= patrolPoints [0].position.x) {
			direction = -1;
		} else if (transform.position.x <= patrolPoints[1].position.x && transform.position.x < patrolPoints[0].position.x) {
			direction = 1;
		}
	}

	void Detect (int target) { // Is played while the enemy is detecting the player
		detectionLevel = Mathf.SmoothDamp (detectionLevel, target, ref detectionVelocity, detectionTime);
		detectionLevel = Mathf.Clamp (detectionLevel, 0, 100);
		detectionImage.fillAmount = detectionLevel / 100;

		if (detectionLevel < 100) {
			enemyState = state.Alert;
		} else if (detectionLevel > 100) {
			enemyState = state.Persuing;
		}
	}

	void OnTriggerStay2D (Collider2D other) { // detects player
		if (other != null && other.gameObject.tag == "Player" && other.gameObject.layer == 9) {
			target = other.gameObject;
			playerDetected = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) { // player is no longer being detected
		if (other != null && other.gameObject.tag == "Player") {
			playerDetected = false;
		}
	}

	void Pursue () { // When the enemy is persuing the player
		if (playerLevel != level) {
			enemyState = state.Switching;
		} else {
			if (transform.position.x > target.transform.position.x) {
				direction = -1;
			} else {
				direction = 1;
			}
		}

		if (shootingEnabled == true && canShoot == true && level == playerLevel) {
			Shoot ();
		} else if (lungingEnabled == true && distanceFromPlayer < lungeDistance && m_Speed > 0) {
			m_Speed = initialSpeed * lungeMultiplier;
		}
		if (distanceFromPlayer > lungeDistance && m_Speed > 0) {
			m_Speed = initialSpeed;
		}
	}

	void Shoot () { // Played when the enemy shoots at the player
		if (Time.time > timeToShoot && Vector2.Distance(transform.position, target.transform.position) < maxDist) {
			shootAnimTimeLeft = Time.time + shootAnimTime;
			teleportersScript.shootSound.Play ();
			timeToShoot = Time.time + fireDelay;
			StartCoroutine (waitAndMove (shootFreezeTime));
			print ("Test");
			GameObject shot = Instantiate (bullet, firePoint.position, transform.rotation) as GameObject;
			Vector3 targetPos = new Vector3 (target.transform.position.x, target.transform.position.y + Random.Range (-randomY, randomY), 0);
			Vector2 dir = targetPos - shot.transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			shot.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			isShooting = true;
		}
	}

	void SwitchLevel () { // Played when the enemy needs to switch level to catch the player
		float TeleporterDistance1 = Vector2.Distance(transform.position, teleportersScript.levelArray[level].teleporter1.position);
		float TeleporterDistance2 = Vector2.Distance(transform.position, teleportersScript.levelArray[level].teleporter2.position);
		Transform targetTeleporter;

		if (playerLevel != level) {
			if (TeleporterDistance1 < TeleporterDistance2) {
				targetTeleporter = teleportersScript.levelArray [level].teleporter1;
			} else {
				targetTeleporter = teleportersScript.levelArray [level].teleporter2;
			}

			if (transform.position.x >= targetTeleporter.position.x) {
				direction = -1;
			} else if (transform.position.x <= targetTeleporter.position.x) {
				direction = 1;
			}
		}
	}



	public void Teleport() { //When the enemy needs to teleport from one level to another
		if (enemyState == state.Switching) {
			if (playerLevel > level) {
				transform.position = new Vector2 (teleportersScript.levelArray [level + 1].teleporter1.transform.position.x, teleportersScript.levelArray [level + 1].teleporter1.transform.position.y);
			} else if (playerLevel < level) {
				transform.position = new Vector2 (teleportersScript.levelArray [level - 1].teleporter1.transform.position.x, teleportersScript.levelArray [level - 1].teleporter1.transform.position.y);
				print ("Test1");
			} else {
				enemyState = state.Persuing;
			}
		}
	}

	IEnumerator waitAndMove (float waitTime) { // A delay to move for when the player shoots
		m_Speed = 0;
		yield return new WaitForSeconds (waitTime);
		m_Speed = initialSpeed;
	}

}