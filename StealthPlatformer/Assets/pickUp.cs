using UnityEngine;
using System.Collections;

public class pickUp : MonoBehaviour {
	Stats statScript;
	public float respawnTime;
	float timeToRespawn;
	bool canPick;
	// Use this for initialization
	void Start () {
		statScript = GameObject.FindGameObjectWithTag ("Gm").GetComponent<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		canPick = Time.time > timeToRespawn;
		GetComponent<SpriteRenderer> ().enabled = canPick;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && canPick) {
			statScript.teleporterPickups++;
			timeToRespawn = Time.time + respawnTime;
		}

	}
}
