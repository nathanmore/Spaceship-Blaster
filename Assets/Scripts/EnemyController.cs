using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : Spaceship
{
    [SerializeField]
    private Vector3 targetLocation;
    [SerializeField]
    private Vector3 minDistance = (0.2f)*Vector3.one;

    private bool locationReached = false;
    private bool weaponFiringActive = true;


    new public void Update()
    {
        base.Update();

        MoveToLocation(targetLocation);

        if (locationReached)
        {
            StartCoroutine(FireWeapon());
        }
        else
        {
            StopCoroutine(FireWeapon());
        }

    }

    public void MoveToLocation(Vector3 targetLoc)
    {
        Vector3 curLoc = this.gameObject.transform.position;
        Vector3 distance = targetLoc - curLoc;
        Vector2 moveDirection2D = new Vector2(distance.normalized.x, distance.normalized.y);
        shipModelTransform.rotation = baseRotation; // Resets rotation (for tilt) to normal before every move

        if (distance.x > minDistance.x || distance.y > minDistance.y) // If not within minimum distance to target location
        {
            Move(moveDirection2D);
            Tilt(moveDirection2D, 25); // Tilt 25 degrees around ship's y-axis while moving
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
            yield return new WaitForSeconds(shipData.fireDelay);
            weaponFiringActive = true;
        }
        else
        {
            yield return null;
        }
    }
}

// Delegate for handling events
public delegate void EnemyDelegate(EnemyController enemyRef);
