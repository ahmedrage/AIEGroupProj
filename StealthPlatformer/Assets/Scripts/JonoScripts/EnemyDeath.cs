using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    private void OnCollisionEnter2D(Collision2D other)
    {
       if  (other.gameObject.tag == "Bullet") 
        {
            Rigidbody2D targetRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
			Destroy (other.gameObject);
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update () {
	
	}
}
