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

    public int DodgeRotation;

    public int scoreValue;

    public float fireDelay;

    public int weaponDamage;

    public SoundManager.Sound laserAudio;

    public float laserVolumeOffset = 1.0f;

    public SoundManager.Sound hitAudio;

    public float hitVolumeOffset = 1.0f;

    public SoundManager.Sound destructionAudio;

    public float destructionVolumeOffset = 1.0f;
}
