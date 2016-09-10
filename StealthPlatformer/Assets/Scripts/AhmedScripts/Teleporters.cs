using UnityEngine;
using System.Collections;
[System.Serializable]
public class level {
	public float yPos;
	public Transform teleporter1;
	public Transform teleporter2;
}

public class Teleporters : MonoBehaviour {
	public level[] levelArray;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
