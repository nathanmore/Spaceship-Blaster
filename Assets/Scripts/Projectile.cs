using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 10f;

    //private Vector3 projectileVelocity;
    private int projectileDamage = 1;
    private bool hit;
    private bool playerFriendly = false;
    private PlayerController playerReference;

    // Public accesssor for projectieDamage, in case future power-ups want to increase damage.
    public int ProjectileDamage { get { return projectileDamage; } set { projectileDamage = value; } }


    // Start is called before the first frame update
    void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == false)
        {  
            Motion(Vector3.up * projectileSpeed);
        }
    }

    // When projectile hits damageable object, set hit to true, deal damage, and destroy projectile
    public void OnCollisionEnter(Collision collision)
    {
        IDamageable<int> impact = collision.gameObject.GetComponent<IDamageable<int>>();

        if (impact != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (playerFriendly != true)
                {
                    impact.Damage(projectileDamage);
                    Destroy(this.gameObject);
                }
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                if (playerFriendly == true)
                {
                    impact.Damage(projectileDamage);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void Motion(Vector3 velocity)
    {
        transform.position += velocity;
    }

    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public void MakePlayerFriendly()
    {
        playerFriendly = true;
    }
}
