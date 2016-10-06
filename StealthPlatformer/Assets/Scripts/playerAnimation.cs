using UnityEngine;
using System.Collections;

public class playerAnimation : MonoBehaviour {
	public Animator gunAnimator;
	public AudioSource shootSound;
	PlayerShoot shootScript;
	public bool isShooting;
	float ammo;
	public float resetDelay;
	float timeToReset;
	// Use this for initialization
	void Start () {
		shootScript = GetComponent<PlayerShoot> ();
		gunAnimator = GameObject.FindGameObjectWithTag ("playerGun").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		ammo = shootScript.Ammo;

		if (Input.GetButtonDown ("Fire1") && ammo > 0) {
			timeToReset = Time.time + resetDelay;
			isShooting = true;
			shootSound.Play ();
		} else if(Time.time > timeToReset ) {
			isShooting = false;
		}

		gunAnimator.SetBool ("isShooting", isShooting);
	}
}
