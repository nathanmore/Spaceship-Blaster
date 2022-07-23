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
    private SO_SpaceshipData shipData; // Data object for spaceship
    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected Transform projectileSpawnTransform;

    //// Called when object first becomes active, before Start
    //public void OnEnable()
    //{
    //    // Clones the master data file so it does not make changes to it during runtime. Must be done before accessing data.
    //    SO_SpaceshipData clone = Instantiate(shipData);
    //    shipData = clone;
    //}

    // Start is called before the first frame update
    public void Start()
    {
        shipData.CurrentHealth = shipData.MaxHealth;
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

        // Tilt 30 degrees around ship's y-axis while moving
        Tilt(direction, 30);
    }

    // Tilts ship around y-axis according to the given angle and which direction it's going
    public void Tilt(Vector2 direction, int angle)
    {
      if(direction.x > 0)
      {
        transform.Rotate(0, (-1) * angle, 0);
      }
      else if(direction.x < 0)
      {
        transform.Rotate(0, angle, 0);
      }
    }

    // Quickly move ship to right if isRight is true, move to left otherwise
    public void Dodge(bool isRight)
    {
        if (isRight)
        {
            Debug.Log("Roll to the right");

            // Change ship's location
            Vector3 velocity = new Vector3(1, 0, 0) * shipData.DodgeSpeed * Time.deltaTime;
            transform.position += velocity;
            // Change ship's rotation (in each frame, tilt 15 degrees to the right)
            Tilt(new Vector2(1, 0), 15);
        }
        else
        {
            Debug.Log("Roll to the left");

            Vector3 velocity = new Vector3(-1, 0, 0) * shipData.DodgeSpeed * Time.deltaTime;
            transform.position += velocity;

            Tilt(new Vector2(-1, 0), 15);
        }
    }

    // Fire projectile (instantiate projectile object)
    public void Fire()
    {
        GameObject.Instantiate(projectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
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
    }
}
