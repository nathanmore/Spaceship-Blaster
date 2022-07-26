using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    private SO_ScoreData scoreData;
    [SerializeField]
    private TextMeshProUGUI scoreTextAsset;
    [SerializeField]
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        scoreTextAsset.text = scoreData.CurrentScore.ToString();
        scoreData.ResetCurrentScore();

        player.shipDestroyedDelegate += OnPlayerDefeated;
    }

    public void UpdateScore(int points)
    {
        scoreData.IncreaseScore(points);
        scoreTextAsset.text = scoreData.CurrentScore.ToString();
    }

    public void OnPlayerDefeated(GameObject playerObj)
    {
        // Pause gameplay, update high score, and display end screen
        Debug.Log("Game Over");
        SceneManager.LoadScene("MainMenu"); // Temporary transition
    }
}
