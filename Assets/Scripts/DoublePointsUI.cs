using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoublePointsUI : MonoBehaviour
{
    [SerializeField]
    SO_ScoreData scoreAsset;
    [SerializeField]
    private float flashingDelay;

    private TextMeshProUGUI UITextComponent;
    private bool restartFlashing = true;

    public void Start()
    {
        UITextComponent = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        if (scoreAsset.DoublePoints == false)
        {
            UITextComponent.enabled = false;
        }
        else
        {
            if (restartFlashing == true)
            {
                StartCoroutine(Flashing2X());
            }
        }
    }

    public IEnumerator Flashing2X()
    {
        restartFlashing = false;
        UITextComponent.enabled = true;

        yield return new WaitForSecondsRealtime(flashingDelay);

        UITextComponent.enabled = false;

        yield return new WaitForSeconds(flashingDelay/2.0f);

        restartFlashing = true;
    }
}
