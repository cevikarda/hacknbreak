using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Text gameOverPanelScoreText;
    [SerializeField]
    private Text gameOverPanelHighScoreText;
    [SerializeField]
    private GameObject gameOverPanelGO;

    public void OnScoreUpdated(int score)
    {
        scoreText.text = score.ToString();
        gameOverPanelScoreText.text = "Score" + Environment.NewLine + score;
    }

    public void OnHighScoreUpdated(int highScore)
    {
        highScoreText.text = highScore.ToString();
        gameOverPanelHighScoreText.text = "High Score" + Environment.NewLine + highScore;
    }

    public void OnGameFinished()
    {
        gameOverPanelGO.SetActive(true);
    }

    public void OnReplayButtonClicked()
    {
        AudioManager.Instance.PlayButtonClickSFX();

        gameOverPanelGO.SetActive(false);
        gameController.OnStartGameIntent();
    }
}