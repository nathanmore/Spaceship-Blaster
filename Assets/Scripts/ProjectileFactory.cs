using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    public Projectile InstantiateProjectile(string objectTag, Vector3 direction, int damage = 1)
    {
        Projectile projectile = GameObject.Instantiate(projectilePrefab, this.transform).GetComponent<Projectile>();

        projectile.ObjectTag = objectTag;
        projectile.Direction = direction;
        projectile.ProjectileDamage = damage;
        projectile.gameObject.transform.parent = null;

        return projectile;
    }
}
