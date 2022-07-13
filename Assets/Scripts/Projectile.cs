using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Vector3 projectileVelocity;

    private bool hit;

    public Vector3 ProjectileVelocity { get { return projectileVelocity; } set { projectileVelocity = value; } }

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
            Motion(projectileVelocity);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // When projectile hits enemy or asteroid, set hit to true, deal damage, and destroy projectile
    }

    public void Motion(Vector3 velocity)
    {
        transform.position += velocity;
    }
}
