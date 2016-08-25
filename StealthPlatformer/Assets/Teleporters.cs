using UnityEngine;
using System.Collections;
[System.Serializable]
public class Teleport {
	public Transform teleportOrigin;
	public Transform upperTarget;
	public Transform lowerTarget;
	public int level;

}

public class Teleporters : MonoBehaviour {
	public Teleport[] teleportArray;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
