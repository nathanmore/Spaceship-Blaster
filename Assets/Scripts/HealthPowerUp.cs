using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField]
    private int healthAward;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                SoundManager.PlaySound(SoundManager.Sound.playerPowerup);

                player.ShipData.CurrentHealth += healthAward;

                if (player.ShipData.CurrentHealth > player.ShipData.MaxHealth)
                {
                    player.ShipData.CurrentHealth = player.ShipData.MaxHealth;
                }

                Destroy(this.gameObject);
            }
        }
    }
}
