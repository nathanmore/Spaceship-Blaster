using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 10f;

    private bool hit;

    public int ProjectileDamage { get; set; }
    public string ObjectTag { get; set; }
    public Vector3 Direction { get; set; }



    // Start is called before the first frame update
    public void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    public void FixedUpdate()
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
            if (collision.gameObject.tag != ObjectTag)
            {
                impact.Damage(ProjectileDamage);
                Destroy(this.gameObject);
            }
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
