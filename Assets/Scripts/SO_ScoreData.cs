using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score-Data", menuName = "ScriptableObjects/SO_ScoreData", order = 1)]
public class SO_ScoreData : ScriptableObject
{
    [SerializeField]
    private int currentScore;
    [SerializeField]
    private int highScore;

    public int CurrentScore { get { return currentScore; } }
    public int HighScore { get { return highScore; } }

    public void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    public void IncreaseScore(int points)
    {
        currentScore += points;
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void UpdateHighScore()
    {
        highScore = currentScore;
        PlayerPrefs.SetInt("HighScore", currentScore);
    }
}
