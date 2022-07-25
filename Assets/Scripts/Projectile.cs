using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 10f;

    private bool hit;
    private bool playerFriendly = false;

    public int ProjectileDamage { get; set; }
    public string ObjectTag { get; set; }
    public Vector3 Direction { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        hit = false;
        if (ObjectTag == "Player")
        {
            playerFriendly = true;
        }
        else
        {
            playerFriendly = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == false)
        {
            transform.position += (Direction * projectileSpeed);
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
                    impact.Damage(ProjectileDamage);
                    Destroy(this.gameObject);
                }
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                if (playerFriendly == true)
                {
                    impact.Damage(ProjectileDamage);
                    Destroy(this.gameObject);
                }
            }
        }
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
