using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int scoreReward;
    public TextMeshProUGUI scoreText;
    private int score;

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

    private void OnEnable()
    {
        GameManager.onStartGame += ResetScore;
        GameManager.onSubmittedOrder += RewardPlayer;
    }

    private void OnDisable()
    {
        GameManager.onStartGame -= ResetScore;
        GameManager.onSubmittedOrder -= RewardPlayer;
    }
}