using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gamewinning2 : MonoBehaviour {
    int index;
    GameWinning gameWinningClass;
    // Use this for initialization
    void Start()
    {
        gameWinningClass = GameObject.FindGameObjectWithTag("Objective").GetComponent<GameWinning>();

    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameWinningClass.Objectivereached == 1 && other.gameObject != null && other.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings)
            {
                index = 0;
            } else
            {
                index = SceneManager.GetActiveScene().buildIndex + 1;

            }

            SceneManager.LoadScene(index);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
