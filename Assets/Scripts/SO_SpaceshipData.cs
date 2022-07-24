using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spaceship-Data", menuName = "ScriptableObjects/SO_SpaceshipData", order = 1)]
public class SO_SpaceshipData : ScriptableObject
{
    public int MaxHealth;

    public int CurrentHealth;

    public float MovementSpeed;

    public float DodgeSpeed;

    public int scoreValue;
}
