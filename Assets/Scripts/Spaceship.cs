using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to contain functions related to all spaceships (player and enemies):
///     Movement
///     Dodging
///     Shooting
/// </summary>
public class Spaceship : MonoBehaviour, IDamageable<float>
{
    [SerializeField]
    SO_SpaceshipData shipData;

    // Called when object is enabled, before Start
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

    // Moves ship on x/y axis according to velocity
    public void Move(Vector2 velocity)
    {

    }

    // quickly move ship to right if isRight is true, move to left otherwise
    public void Dodge(bool isRight)
    {

    }

    // Fire projectile (instantiate projectile object)
    public void Fire()
    {

    }

    // Required method for IDamageable interface
    // Reduces health based on damageTaken
    // If health reaches 0, destroy ship
    public void Damage(float damageTaken)
    {

    }
}
