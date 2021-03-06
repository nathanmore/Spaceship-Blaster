/// <summary>
/// Class to contain functions related to all spaceships (player and enemies):
///     Movement
///     Dodging
///     Shooting
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spaceship : MonoBehaviour, IDamageable<int>
{
    [SerializeField]
    protected SO_SpaceshipData shipData; // Data object for spaceship
    [SerializeField]
    protected ProjectileFactory projectileFactory;
    [SerializeField]
    protected Transform shipModelTransform;

    protected Quaternion baseRotation;
    public SpaceshipDelegate shipDestroyedDelegate;

    public int ScoreValue { get { return shipData.scoreValue; } }

    // Start is called before the first frame update
    public void Start()
    {
        shipData.CurrentHealth = shipData.MaxHealth;

        baseRotation = shipModelTransform.rotation;
    }

    // Update is called once per frame
    public void Update()
    {

    }

    // Moves ship on x/y axis according to normalized vector passed in for direction
    public void Move(Vector2 direction)
    {
        // Change position of ship
        Vector3 velocity = new Vector3(direction.x, direction.y, 0) * shipData.MovementSpeed * Time.deltaTime;
        transform.position += velocity;
    }

    // Tilts ship around y-axis according to the given angle and which direction it's going
    public void Tilt(Vector2 direction, int angle)
    {
      if(direction.x > 0)
      {
        shipModelTransform.Rotate(0, (-1) * angle, 0);
      }
      else if(direction.x < 0)
      {
        shipModelTransform.Rotate(0, angle, 0);
      }
    }

    // Quickly move ship to right if isRight is true, move to left otherwise
    public void Dodge(bool isRight, int tiltRot)
    {
        if (isRight)
        {
            // Change ship's location
            Vector3 velocity = new Vector3(1, 0, 0) * shipData.DodgeSpeed * Time.deltaTime;
            transform.position += velocity;
            // Change ship's rotation (in each frame, tilt 5 degrees to the right)
            Tilt(new Vector2(1, 0), tiltRot);
        }
        else
        {
            Vector3 velocity = new Vector3(-1, 0, 0) * shipData.DodgeSpeed * Time.deltaTime;
            transform.position += velocity;

            Tilt(new Vector2(-1, 0), tiltRot);
        }
    }

    // Fire projectile (instantiate projectile object)
    public void Fire()
    {
        // Get direction the projectile should travel
        Vector3 projectileDirection = (projectileFactory.transform.position - this.gameObject.transform.position).normalized;

        // Instantiate projectile
        projectileFactory.InstantiateProjectile(this.gameObject.tag, projectileDirection, shipData.weaponDamage);
    }

    // Required method for IDamageable interface
    // Reduces health based on damageTaken
    // If health reaches 0, call DestroyShip()
    public void Damage(int damageTaken)
    {
        shipData.CurrentHealth -= damageTaken;

        if (shipData.CurrentHealth <= 0)
        {
            DestroyShip();
        }
    }

    public void DestroyShip()
    {
        Destroy(gameObject);
        if (shipDestroyedDelegate != null)
        {
            shipDestroyedDelegate(this.gameObject);
        }
    }
}

// Delegate for handling events
public delegate void SpaceshipDelegate(GameObject objectRef);