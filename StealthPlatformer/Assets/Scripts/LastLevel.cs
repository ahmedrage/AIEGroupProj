using UnityEngine;
using System.Collections;

public class LastLevel : MonoBehaviour {
	public int lastLevel = 1;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
