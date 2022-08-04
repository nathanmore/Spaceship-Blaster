using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointsPowerUp : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ActivateDoublePoints();
                Destroy(this.gameObject);
            }
        }
    }
}
