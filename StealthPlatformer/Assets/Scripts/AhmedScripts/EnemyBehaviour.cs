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

	public bool playerDetected;
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
		playerLevel = GameObject.FindGameObjectWithTag ("Player").GetComponent<dummyPlayerScript> ().level;
		if (playerDetected == false && detectionLevel > 0 && detectionLevel < 100) {
			Detect (-1);
		} else if (playerDetected == true && detectionLevel < 100) {
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

	void Detect (int target) {
		detectionLevel = Mathf.SmoothDamp (detectionLevel, target, ref detectionVelocity, detectionTime);
		detectionLevel = Mathf.Clamp (detectionLevel, 0, 100);
		detectionImage.fillAmount = detectionLevel / 100;

		if (detectionLevel < 100) {
			enemyState = state.Alert;
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
				transform.position = new Vector2 (transform.position.x, teleportersScript.levelArray [level + 1].yPos);
				level++;
			} else if (playerLevel < level) {
				transform.position = new Vector2 (transform.position.x, teleportersScript.levelArray [level - 1].yPos);
				level--;
			} else {
				enemyState = state.Persuing;
			}
		}
	}
}