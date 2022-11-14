using UnityEngine;
using TMPro;
using GoogleMobileAds.Api;

public class GameDemoManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public GameObject ball;
    public TMP_Text _scoreLabel;

    private RewardedAdController rewardedAdController;
    private float _score;

    // Start is called before the first frame update
    void Start()
    {
        rewardedAdController = GetComponent<RewardedAdController>();

        _score = 0;
        VolleyBall.OnClickToPlay += GoToInGame;
        VolleyBall.OnVolley += IncrementScore;
        VolleyBall.OnFallToBottom += GoToGameOver;

        GoToMainMenu();
    }

    void IncrementScore()
    {
        _score++;
        UpdateScore();
    }

    public void HandleRewardedAdButtonClick()
    {
        // Show Rewarded Ad
        rewardedAdController.HandleShowRewarded();

        rewardedAdController.rewardedAd.OnUserEarnedReward += DoubleScore;
    }

    void DoubleScore(object sender, Reward reward)
    {
        _score *= 2;
        UpdateScore();
    }

    void UpdateScore() => _scoreLabel.text = _score.ToString();

    public void GoToMainMenu()
    {
        Time.timeScale = 0; // Pause Game

        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        inGamePanel.SetActive(false);

        // reset game
        ball.transform.position = Vector2.zero;
        _score = 0;
        UpdateScore();
        Time.timeScale = 0;
    }

    void GoToInGame()
    {
        Time.timeScale = 1; // Start Game

        inGamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void GoToGameOver()
    {
        Time.timeScale = 0; // Pause Game
        gameOverPanel.SetActive(true);
        inGamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
}
