using UnityEngine;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    public float m_Speed = 10; //Speed of the player
    private float m_MovementInputValue; //Value of movement input
    public Rigidbody2D m_Rigidbody;
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // when the tank is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;
        // also reset the input values
        m_MovementInputValue = 0f;
    }

    private void OnDisable()
    {
        // when the tank is turned off, set it to kinematic so it stops moving
        m_Rigidbody.isKinematic = true;
    }
    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        // create a vector in the direction the tank is facing with a magnitude of m_Speed
        // based on the input, speed and time between frames
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * m_Speed * Time.deltaTime, m_Rigidbody.velocity.y);
        // Apply this movement to the rigidbody's position
        m_Rigidbody.velocity = movement;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
}
