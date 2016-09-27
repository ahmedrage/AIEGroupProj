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
	public Transform player;
	public int playerLevel;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		float yPos = player.position.y;
		int i = 0;
		foreach (var Level in levelArray) {
			if (i == 0) {
				if (yPos == levelArray [i].teleporter1.position.y || yPos < levelArray [i + 1].teleporter1.position.y) {
					playerLevel = i;
					return;
				}
			} else {
				if (yPos == levelArray [i].teleporter1.position.y || (yPos < levelArray [i + 1].teleporter1.position.y && yPos > levelArray [i - 1].teleporter1.position.y)) {
					playerLevel = i;
					return;
				}
			}
			i++;
		}		
		player.GetComponent<dummyPlayerScript> ().level = playerLevel;
	}
		
}

