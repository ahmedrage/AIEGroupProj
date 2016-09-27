using UnityEngine;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    public Vector2 jumpHeight;
    public int level = 0;
    public float m_Speed = 10; //Speed of the player
    private float m_MovementInputValue; //Value of movement input
    public Rigidbody2D m_Rigidbody;
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>(); //waken the rigidbody of the player
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false; //allow the player to move
        m_MovementInputValue = 0f;
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true; //Disallow movement
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * m_Speed * Time.deltaTime, m_Rigidbody.velocity.y);
        m_Rigidbody.velocity = movement;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>(); //Fix for if the away function breaks
        
    // Update is called once per frame
        if (Input.GetKeyDown(KeyCode.UpArrow) && Mathf.Abs(m_Rigidbody.velocity.y) < 0.1)  //makes player jump
        {
            GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
			GameObject.FindGameObjectWithTag ("Gm").GetComponent<Stats> ().isDead = true;
			//Destroy(gameObject);
        }
    }
}
