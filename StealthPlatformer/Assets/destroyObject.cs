using UnityEngine;
using System.Collections;

public class destroyObject : MonoBehaviour {
	public float delay;
	float timeToDestroy;
	// Use this for initialization
	void Awake () {
		timeToDestroy = Time.time + timeToDestroy;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeToDestroy) {
			Destroy (gameObject);
		}
	}
}
