using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public Rigidbody2D m_rigidbody2d; //Enemy rigidbody
	public enum state // Defining the enemy states
	{
		Patrolling,
		Alert,
		Persuing
	};
	public float m_Speed;
	public int direction = 1;

	public Transform[] patrolPoints;

	public state enemyState; // The current state of the enemy

	void Start () {
		m_rigidbody2d = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		if (enemyState == state.Patrolling) {
			if (transform.position.x >= patrolPoints [0].position.x) {
				direction = -1;
			} else if (transform.position.x <= patrolPoints[1].position.x) {
				direction = 1;
			}

			m_rigidbody2d.velocity = Vector2.right * direction * m_Speed;
		
		}
	}
}
