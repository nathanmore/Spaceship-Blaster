using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private SO_SpaceshipData playerShipData;
    [SerializeField]
    private Image barFill;

    public void Start()
    {
        //slider.maxValue = playerShipData.MaxHealth;
        //slider.value = playerShipData.CurrentHealth;

        //fill.color = gradient.Evaluate(1f);

        
    }

    public void Update()
    {
        SetHealth();
    }

    public void SetHealth()
    {
        barFill.fillAmount = (float)playerShipData.CurrentHealth / (float)playerShipData.MaxHealth;

        //slider.value = playerShipData.CurrentHealth;

        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
   
}
