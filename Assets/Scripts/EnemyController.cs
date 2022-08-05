using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : Spaceship
{
    [SerializeField]
    private Vector3 targetLocation;
    [SerializeField]
    private Vector3 minDistance = (0.1f)*Vector3.one;

    private bool locationReached = false;
    private bool weaponFiringActive = true;

    // Called when object is enabled, before Start
    public void OnEnable()
    {
        // Clones the master data file so it does not make changes to it during runtime.
        SO_SpaceshipData clone = Instantiate(shipData);
        shipData = clone;
    }

    new public void Update()
    {
        base.Update();

        if (locationReached)
        {
            StartCoroutine(FireWeapon());
        }
        else
        {
            StopCoroutine(FireWeapon());
        }

    }

    public void FixedUpdate()
    {
        MoveToLocation(targetLocation);
    }

    public void MoveToLocation(Vector3 targetLoc)
    {
        Vector3 curLoc = this.gameObject.transform.position;
        Vector3 distance = targetLoc - curLoc;
        Vector2 moveDirection2D = new Vector2(distance.normalized.x, distance.normalized.y);
        shipModelTransform.rotation = baseRotation; // Resets rotation (for tilt) to normal before every move

        if (Mathf.Abs(distance.x) > minDistance.x || Mathf.Abs(distance.y) > minDistance.y) // If not within minimum distance to target location
        {
            locationReached = false;
            Move(moveDirection2D);
            Tilt(moveDirection2D, -25); // Tilt 25 degrees around ship's y-axis while moving
        } 
        else
        {
            locationReached = true;
        }
    }

    public void SetTargetLocation(Vector3 newLoc)
    {
        targetLocation = newLoc;
    }

    public IEnumerator FireWeapon()
    {
        if (weaponFiringActive)
        {
            Fire();
            weaponFiringActive = false;
            float delay = Random.Range(shipData.fireDelay * 0.75f, shipData.fireDelay * 1.25f);
            yield return new WaitForSeconds(shipData.fireDelay);
            weaponFiringActive = true;
        }
        else
        {
            yield return null;
        }
    }
}
