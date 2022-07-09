using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spaceship-Data", menuName = "ScriptableObjects/SO_SpaceshipData", order = 1)]
public class SO_SpaceshipData : ScriptableObject
{
    public float health;

    public float movementSpeed;

    public float dodgeSpeed;


    // Accessors for private variables
    public float Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float DodgeSpeed { get { return dodgeSpeed; } }
}
