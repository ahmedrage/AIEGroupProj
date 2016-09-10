using UnityEngine;
using System.Collections;
[System.Serializable]
public class Level {
	public float yPos;
	public Transform teleporter1;
	public Transform teleporter2;
}

public class Stats : MonoBehaviour {
	public Level[] levelArray;
	public bool detected;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
