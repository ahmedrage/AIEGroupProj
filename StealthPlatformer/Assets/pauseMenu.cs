using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class pauseMenu : MonoBehaviour {
	public GameObject pausePannel;
	// Use this for initialization
	void Start () {
		pausePannel.SetActive (false);
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			resumeGame (!pausePannel.activeSelf);
		}
	}

	public void loadMenu () {
		SceneManager.LoadScene (0);
	}
	public void resumeGame (bool isEnable) {
		pausePannel.SetActive (isEnable);
		if (!isEnable) {
			Time.timeScale = 1;
		} else {
			Time.timeScale = 0;
		}
	}
}
