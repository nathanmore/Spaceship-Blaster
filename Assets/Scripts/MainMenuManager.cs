using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private SO_ScoreData scoreData;

    public void Start()
    {
        // Update highScoreText to display current high score
        highScoreText.text = scoreData.HighScore.ToString();
    }

    // Start new game: transition to main scene
    public void PlayGame()
    {
        SceneManager.LoadScene("main");
    }

    // Open the options menu
    public void Options()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
