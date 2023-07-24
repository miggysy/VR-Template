using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int scoreReward;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    private int score;
    private int bestScore;

    private void Start()
    {
        GetBestScore();
    }

    private void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void RewardPlayer()
    {
        AddScore(scoreReward);
    }

    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }

    private void GetBestScore()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        bestScoreText.text = bestScore.ToString();
    }

    private void SetBestScore()
    {
        bestScore = score;
        PlayerPrefs.SetInt("bestScore", bestScore);
        bestScoreText.text = bestScore.ToString();
    }

    private void OnEnable()
    {
        GameManager.onStartGame += ResetScore;
        GameManager.onSubmittedOrder += RewardPlayer;
        GameManager.onGameOver += SetBestScore;
    }

    private void OnDisable()
    {
        GameManager.onStartGame -= ResetScore;
        GameManager.onSubmittedOrder -= RewardPlayer;
        GameManager.onGameOver -= SetBestScore;
    }
}