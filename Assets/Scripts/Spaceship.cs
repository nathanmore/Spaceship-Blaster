using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to contain functions related to all spaceships (player and enemies):
///     Movement
///     Dodging
///     Shooting
/// </summary>
public class Spaceship : MonoBehaviour, IDamageable<int>
{
    [SerializeField]
    SO_SpaceshipData shipData; // Data object for spaceship

    // Called when object first becomes active, before Start
    public void OnEnable()
    {
        // Clones the master data file so it does not make changes to it during runtime. Must be done before accessing data.
        SO_SpaceshipData clone = Instantiate(shipData);
        shipData = clone;
    }

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    // Moves ship on x/y axis according to normalized vector passed in for direction
    public void Move(Vector2 directon)
    {

    }

    // quickly move ship to right if isRight is true, move to left otherwise
    public void Dodge(bool isRight)
    {
        if (isRight)
        {
            Debug.Log("Roll to the right");
        }
        else
        {
            Debug.Log("Roll to the left");
        }
    }

    // Fire projectile (instantiate projectile object)
    public void Fire()
    {
        Debug.Log("Fire Gun");
    }

    // Required method for IDamageable interface
    // Reduces health based on damageTaken
    // If health reaches 0, call DestroyShip()
    public void Damage(int damageTaken)
    {
        shipData.Health -= damageTaken;
        
        if (shipData.Health <= 0)
        {
            DestroyShip();
        }
    }

    public void DestroyShip()
    {
        Debug.Log("Ship has been destroyed.");
        Destroy(gameObject);
    }
}
