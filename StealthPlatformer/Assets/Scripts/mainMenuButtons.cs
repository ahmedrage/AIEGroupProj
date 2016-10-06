using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class mainMenuButtons : MonoBehaviour {
	public void StartGame () {
		SceneManager.LoadScene (GameObject.FindGameObjectWithTag("LastLevel").GetComponent<LastLevel>().lastLevel);	
	}
	public void ExitGame () {
		Application.Quit ();
	}
}
