using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D targetRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
        // Add Damage Code Here
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
