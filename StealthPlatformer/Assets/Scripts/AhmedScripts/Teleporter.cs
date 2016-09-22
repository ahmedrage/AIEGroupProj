using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
	public GameObject player;
	public float maxTeleporterRange;
	int playerLevel;
	public Stats teleportersScript;
	public float teleportDelay;
	float timeToTeleport;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		teleportersScript = GameObject.FindGameObjectWithTag ("Gm").GetComponent<Stats> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeToTeleport = teleportersScript.timeToTeleport;
		playerLevel = teleportersScript.playerLevel;

		if (Vector2.Distance (player.transform.position, transform.position) < maxTeleporterRange & Time.time > timeToTeleport) {
				
			if (Input.GetKeyDown (KeyCode.W) && playerLevel < teleportersScript.levelArray.Length - 1) {
				float xPos;
				float tp1XPos = teleportersScript.levelArray [playerLevel + 1].teleporter1.transform.position.x;
				float tp2XPos = teleportersScript.levelArray [playerLevel + 1].teleporter2.transform.position.x;
				if (Mathf.Abs (transform.position.x - tp1XPos) > Mathf.Abs (transform.position.x - tp2XPos)) {
					xPos = tp2XPos;
				} else {
					xPos = tp1XPos;
				}
				player.transform.position = new Vector2 (xPos, teleportersScript.levelArray [playerLevel + 1].teleporter1.transform.position.y);
				timeToTeleport = Time.time + teleportDelay;
				teleportersScript.timeToTeleport = timeToTeleport;
			} else if (Input.GetKeyDown (KeyCode.S) && playerLevel > 0) {
				float xPos;
				float tp1XPos = teleportersScript.levelArray [playerLevel - 1].teleporter1.transform.position.x;
				float tp2XPos = teleportersScript.levelArray [playerLevel - 1].teleporter2.transform.position.x;
				if (Mathf.Abs (transform.position.x - tp1XPos) > Mathf.Abs (transform.position.x - tp2XPos)) {
					xPos = tp2XPos;
				} else {
					xPos = tp1XPos;
				}

				player.transform.position = new Vector2 (xPos, teleportersScript.levelArray [playerLevel - 1].teleporter1.transform.position.y);
				timeToTeleport = Time.time + teleportDelay;
				teleportersScript.timeToTeleport = timeToTeleport;
			}
		}
	}


	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy" && other.gameObject.GetComponent<EnemyBehaviour>() != null) {
			other.gameObject.GetComponent<EnemyBehaviour> ().Teleport ();
		}
	}
}
