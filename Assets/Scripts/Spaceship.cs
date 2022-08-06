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
    [SerializeField]
    private float impactImpulse = 40.0f;

    protected Quaternion baseRotation;
    public SpaceshipDelegate shipDestroyedDelegate;
    protected bool movementEnabled = true;
    public bool isDamageable = true;

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
        if (movementEnabled)
        {
            // Change position of ship
            Vector3 velocity = new Vector3(direction.x, direction.y, 0) * shipData.MovementSpeed * Time.deltaTime;
            transform.position += velocity;
        }
    }

    // Tilts ship around y-axis according to the given angle and which direction it's going
    public void Tilt(Vector2 direction, int angle)
    {
        if (movementEnabled)
        {
            if (direction.x > 0)
            {
                shipModelTransform.Rotate(0, (-1) * angle, 0);
            }
            else if (direction.x < 0)
            {
                shipModelTransform.Rotate(0, angle, 0);
            }
        }
    }

    // Fire projectile (instantiate projectile object)
    public void Fire()
    {
        // Get direction the projectile should travel
        Vector3 projectileDirection = (projectileFactory.transform.position - this.gameObject.transform.position).normalized;

        // Instantiate projectile
        projectileFactory.InstantiateProjectile(this.gameObject.tag, projectileDirection, shipData.weaponDamage);

        SoundManager.PlaySound(shipData.laserAudio, shipData.laserVolumeOffset);
    }

    // Required method for IDamageable interface
    // Reduces health based on damageTaken
    // If health reaches 0, call DestroyShip()
    public void Damage(int damageTaken)
    {
        if (isDamageable)
        {
            shipData.CurrentHealth -= damageTaken;

            SoundManager.PlaySound(shipData.hitAudio, shipData.hitVolumeOffset);

            StartCoroutine(DamageFlash()); // Coroutine to make ship flash red when damaged

            if (shipData.CurrentHealth <= 0)
            {
                DestroyShip();
            }
        }
    }

    public IEnumerator DamageFlash()
    {
      // Get list of renderers associated w/ the game objects that make up the ship
      // Create new list to hold the original colors of each of these objects
      Renderer[] renderers = GetComponentsInChildren<Renderer>();
      List<Color> colorList = new List<Color>();

      foreach(Renderer r in renderers) // Loop through each renderer (object) in the ship
      {
        //Debug.Log(r.gameObject);
        colorList.Add(r.material.color); // Add object's current color to list
        r.material.color = Color.red; // Change object's color to red
      }
      yield return new WaitForSeconds(0.2f); // Hold red color for 0.2 seconds to create flash effect

      // Loop through both lists again, returning each object's color to its original
      for(int i = 0; i < colorList.Count; i++)
      {
        renderers[i].material.color = colorList[i];
      }
    }

    public void DestroyShip()
    {
        SoundManager.PlaySound(shipData.destructionAudio, shipData.destructionVolumeOffset);
        Destroy(gameObject);
        if (shipDestroyedDelegate != null)
        {
            shipDestroyedDelegate(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Spaceship collidingShip = collision.gameObject.GetComponent<Spaceship>();
        if (collidingShip != null)
        {
            collidingShip.Damage(1);

            Vector3 impactDirection = (collidingShip.transform.position - this.transform.position).normalized;

            StartCoroutine(DisableMovement((impactImpulse/200f)));
            //collidingShip.transform.position += impactDirection * impactImpulse;
            this.transform.rotation = baseRotation;
            this.transform.position += -impactDirection * impactImpulse * Time.deltaTime;
        }
    }

    public IEnumerator DisableMovement(float seconds)
    {
        movementEnabled = false;
        yield return new WaitForSeconds(seconds);
        movementEnabled = true;
    }
}

// Delegate for handling events
public delegate void SpaceshipDelegate(GameObject objectRef);
