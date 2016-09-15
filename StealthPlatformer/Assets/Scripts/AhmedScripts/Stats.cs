using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[System.Serializable]
public class Level {
	public float yPos;
	public Transform teleporter1;
	public Transform teleporter2;
}

public class Stats : MonoBehaviour {
	public Level[] levelArray;
	public bool detected;
	public bool isDead;

	GameObject deathScreen;
	// Use this for initialization
	void Start () {
		if (deathScreen == null) {
			deathScreen = GameObject.FindGameObjectWithTag ("deathScreen");
		}
		deathScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			deathScreen.SetActive (true);
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (0);
			}
		}
	}
}
