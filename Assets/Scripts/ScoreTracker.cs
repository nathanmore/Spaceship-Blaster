using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    private SO_ScoreData scoreData;

    [SerializeField]
    private TextMeshProUGUI scoreTextAsset;

    // Start is called before the first frame update
    void Start()
    {
        scoreTextAsset.text = scoreData.CurrentScore.ToString();
        scoreData.ResetCurrentScore();
    }

    public void UpdateScore(int points)
    {
        scoreData.IncreaseScore(points);
        scoreTextAsset.text = scoreData.CurrentScore.ToString();
    }
}
