using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTry : MonoBehaviour
{
    private Spaceship playerShip;

    //public int maxHealth = 100;
    //public int currentHealth;

    //public HealthBar healthBar;

    void Start()
    {
        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        playerShip = this.gameObject.GetComponent<Spaceship>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TakeDamage(20);
            playerShip.Damage(1);
        }
    }

    //void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;

    //    healthBar.SetHealth(currentHealth);
    //}

}
