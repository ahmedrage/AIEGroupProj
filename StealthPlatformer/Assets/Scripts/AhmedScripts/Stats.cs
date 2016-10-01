using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[System.Serializable]
public class Level {
	public Transform teleporter1;
	public Transform teleporter2;
}

public class Stats : MonoBehaviour {
	public Level[] levelArray;
	public bool detected;
	public bool isDead;
	public float timeToTeleport;
	public Transform player;
	public int playerLevel;
	GameObject deathScreen;
	public AudioSource deathSound;
	public AudioSource shootSound;
	public float playerAmmo;
	// Use this for initialization
	void Start () {
		if (deathScreen == null) {
			deathScreen = GameObject.FindGameObjectWithTag ("deathScreen");
		}
		deathScreen.SetActive (false);

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerAmmo = player.GetComponent<PlayerShoot> ().Ammo;
		Physics2D.IgnoreLayerCollision (13, 9);
		Physics2D.IgnoreLayerCollision (13, 10);

	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			deathScreen.SetActive (true);
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (0);
			}
		}

		if (player != null) {
			if (playerAmmo > 0 && Input.GetButtonDown ("Fire1")) {
				shootSound.Play (); 
			}
			playerAmmo = player.GetComponent<PlayerShoot> ().Ammo;
			playerLevel = checkLevel (player.transform.position.y);
		}
	}

	public int checkLevel (float yPos) {
		int i = 0;
		foreach (var Level in levelArray) {
			if (i == 0) {
				if (Mathf.Round (yPos) == Mathf.Round (levelArray [i].teleporter1.position.y) || yPos < levelArray [i + 1].teleporter1.position.y) {
					return i;
				} 
			} else if (i == levelArray.Length - 1) {
				if (Mathf.Round (yPos) == Mathf.Round (levelArray [i].teleporter1.position.y) || yPos > levelArray [i - 1].teleporter1.position.y) {
					return i;
				}
			}else {
				if (Mathf.Round (yPos) == Mathf.Round (levelArray [i].teleporter1.position.y) || (yPos < levelArray [i + 1].teleporter1.position.y && yPos > levelArray [i - 1].teleporter1.position.y)) {
					return i;
				}
			}
			i++;
		}
		return 0;
	}
}
