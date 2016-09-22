using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
	public float xOffset;
	public float xOffsetPercent;
	public float yOffset;
	public Transform playerTransform;
	public float smoothTime = 0.5f;
	Vector3 currentVelocity;
	public float DELETEME;
	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		xOffset = -Vector2.Distance (Camera.main.ScreenToWorldPoint (new Vector2(0, 0)), Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, 0) * (xOffsetPercent / 100)));

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playerTransform != null) {
			Vector3 target = new Vector3 (playerTransform.position.x + xOffset, playerTransform.position.y + yOffset, transform.position.z); 
			transform.position = Vector3.SmoothDamp (transform.position, target, ref currentVelocity, smoothTime);
		}
	}

	void Update () {
		DELETEME = Input.GetAxisRaw ("Horizontal");
		if (Input.GetAxis ("Horizontal") > 0) {
			xOffset = Mathf.Abs (xOffset);
		} else if (Input.GetAxis ("Horizontal") < 0) {
			xOffset = Mathf.Abs (xOffset) * -1;
		}
	}
}
