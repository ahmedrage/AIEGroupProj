using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy" && other.gameObject.GetComponent<EnemyBehaviour>() != null) {
			other.gameObject.GetComponent<EnemyBehaviour> ().Teleport ();
		}
		if (other.tag == "Player") {
			int playerLevel = other.gameObject.GetComponent<dummyPlayerScript> ().level;
			Stats teleportersScript = GameObject.FindGameObjectWithTag ("Gm").GetComponent<Stats> ();
			if (Input.GetKeyDown(KeyCode.W) && playerLevel < teleportersScript.levelArray.Length - 1) {
				other.gameObject.transform.position = new Vector2 (teleportersScript.levelArray[playerLevel + 1].teleporter1.transform.position.x, teleportersScript.levelArray [playerLevel + 1].teleporter1.transform.position.y);
				other.gameObject.GetComponent<dummyPlayerScript> ().level++;
			} else if (Input.GetKeyDown(KeyCode.S) && playerLevel > 0) {
				other.gameObject.transform.position = new Vector2 (teleportersScript.levelArray[playerLevel - 1].teleporter1.transform.position.x, teleportersScript.levelArray [playerLevel - 1].teleporter1.transform.position.y);
				other.gameObject.GetComponent<dummyPlayerScript> ().level--;
			}
		}
	}
}
