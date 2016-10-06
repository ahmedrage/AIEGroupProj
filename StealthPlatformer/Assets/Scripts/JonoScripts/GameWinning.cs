using UnityEngine;
using System.Collections;

public class GameWinning : MonoBehaviour {
    public int Objectivereached = 0;
    // Use this for initialization
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Objectivereached =+ 1;
            Destroy(gameObject);
            print("woot1");
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
