using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MenuButtons
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private SO_ScoreData scoreData;

    public void Awake()
    {
        OptionSettings.SetupOptions(); // Sets saved data for options 
    }

    public void Start()
    {
        // Update highScoreText to display current high score
        highScoreText.text = scoreData.HighScore.ToString();
    }
}
