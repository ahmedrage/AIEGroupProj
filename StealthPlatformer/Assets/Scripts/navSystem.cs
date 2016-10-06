﻿using UnityEngine;
using System.Collections;

public class navSystem : MonoBehaviour {
	public Transform player;
	public Transform target;
	public float  displayTime;
	public bool followPlayer;
	public GameWinning winScript;

	bool lineDrawn;
	Vector3[] positions;
	float timeToHide;
	LineRenderer m_lineRenderer;
	void Start () {
		m_lineRenderer = GetComponent<LineRenderer> ();
		m_lineRenderer.enabled = false;
		winScript = GameObject.FindGameObjectWithTag ("Objective").GetComponent<GameWinning> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (winScript.Objectivereached == 1) { 
			target = GameObject.FindGameObjectWithTag ("Destination").transform;
		} else {
			target = GameObject.FindGameObjectWithTag ("Objective").transform;
		}

		if (player) {
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				DrawLine (player.transform.position, target.transform.position);
			}
			if (followPlayer == true) { 
				m_lineRenderer.SetPosition (0, player.position);
			}
			if (lineDrawn == true && Time.time > timeToHide) {
				lineDrawn = false;
				m_lineRenderer.enabled = false;
			}
		}
	}

	void DrawLine (Vector3 position1, Vector3 position2) {
		m_lineRenderer.enabled = true;
		timeToHide = Time.time + displayTime;
		lineDrawn = true;
		positions = new [] {position1, position2};
		m_lineRenderer.SetPositions (positions);
	}
}