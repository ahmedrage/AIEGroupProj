using UnityEngine;
using System.Collections;

public class GunPos : MonoBehaviour
{
 public float m_DampTime = 0.2f;
    public Transform m_target;
    private Vector2 m_MoveVelocity;
    private Vector2 m_DesiredPosition;
    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        m_DesiredPosition = m_target.position;
        transform.position = Vector2.SmoothDamp(transform.position,
        m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }
}