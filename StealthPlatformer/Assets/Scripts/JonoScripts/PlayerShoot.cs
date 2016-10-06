using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public Rigidbody2D m_Bullet;
    public Transform m_FireTransform;
    public float m_Launchforce = 3f;
    public float fireRate = 0f;
    public float Ammo = 3;
	int direction = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (transform.localScale.x > 0) {
			direction = 1;
		} else if (transform.localScale.x < 0) {
			direction = -1;
		}
		
        fireRate -= 0.1f;
		if (Input.GetButtonUp("Fire1") && Time.timeScale == 1)
        {
            if (fireRate <= 0 && Ammo > 0)
            {
                Fire();
                fireRate = 1;
                Ammo -= 1;
            }
        }
    }
    private void Fire()
    {
		Rigidbody2D bulletInstance = Instantiate(m_Bullet, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody2D;
		bulletInstance.velocity = m_Launchforce * m_FireTransform.right * direction;
    }
}
