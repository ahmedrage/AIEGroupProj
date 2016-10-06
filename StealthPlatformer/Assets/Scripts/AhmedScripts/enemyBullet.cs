using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour {
	public float speed;

	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = transform.right * speed;
	}

	void OnCollisionEnter2D(Collision2D other) {
		Destroy(gameObject);
	}
}
