using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_ScoreData : ScriptableObject
{
    [SerializeField]
    private int currentScore;
    [SerializeField]
    private int highScore;

    public int CurrentScore { get { return currentScore; } }
    public int HighScore { get { return highScore; } }

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
    }
}
