using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public float rotationSpeed= 50; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, transform.position, rotationSpeed * Time.deltaTime);
	}
}
