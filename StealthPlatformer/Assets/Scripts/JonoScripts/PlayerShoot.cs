using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public Rigidbody2D m_Bullet;
    public Transform m_FireTransform;
    public float m_Launchforce = 3f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }
    private void Fire()
    {
        Rigidbody2D bulletInstance = Instantiate(m_Bullet, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody2D;
        bulletInstance.velocity = m_Launchforce * m_FireTransform.right;
    }
}
