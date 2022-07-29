using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MenuButtons
{
    [SerializeField]
    private SO_ScoreData scoreData;
    [SerializeField]
    private TextMeshProUGUI scoreValueDisplay;
    [SerializeField]
    private GameObject yourScoreText;
    [SerializeField]
    private GameObject newHighScoreText;

    private bool newHighScore = false;

    public void Start()
    {
        if (scoreData.CurrentScore > scoreData.HighScore)
        {
            newHighScore = true;
            scoreData.UpdateHighScore();
        }
        else
        {
            newHighScore = false;
        }

        yourScoreText.SetActive(!newHighScore); // If there is not a new high score, enable this object, otherwise disable it.
        newHighScoreText.SetActive(newHighScore); // If there is not a new high score, disable this object, otherwise enable it.

        scoreValueDisplay.text = scoreData.CurrentScore.ToString();
    }
}
