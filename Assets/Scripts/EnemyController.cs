using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Spaceship
{
    [SerializeField]
    private Vector3 targetLocation;
    [SerializeField]
    private Vector3 minDistance = (0.2f)*Vector3.one;

    new public void Update()
    {
        base.Update();

        MoveToLocation(targetLocation);
    }

    public void MoveToLocation(Vector3 targetLoc)
    {
        Vector3 curLoc = this.gameObject.transform.position;
        Vector3 distance = targetLoc - curLoc;
        Vector2 moveDirection2D = new Vector2(distance.normalized.x, distance.normalized.y);
        transform.rotation = new Quaternion(0, 0, 180, 0); // Resets rotation (for tilt) to normal before every move

        if (distance.x > minDistance.x || distance.y > minDistance.y) // If not within minimum distance to target location
        {
            Move(moveDirection2D);
            Tilt(moveDirection2D, -25); // Tilt 25 degrees around ship's y-axis while moving
        } 
    }

    public void SetTargetLocation(Vector3 newLoc)
    {
        targetLocation = newLoc;
    }
}
