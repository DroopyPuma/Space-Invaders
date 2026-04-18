using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Current player score
    public float score = 0;

    // UI text reference
    [SerializeField]
    private TextMeshProUGUI scoreManagerText;

    public float GetScore()
    {
        return score;
    }

    // Called when enemy is destroyed
    public void AsteroidDestroyed(EnemyController asteroid)
    {
        // Add the enemy's score value
        AddScore(asteroid.GetScoreValue());
    }

    // Adds score
    public void AddScore(float newScore)
    {
        score += newScore;

        Debug.Log("The current score is: " + score);

        // Update UI
        scoreManagerText.text = score.ToString();
    }

    // Set score directly
    public void SetScore(float newScore)
    {
        score = newScore;
        scoreManagerText.text = score.ToString();
    }
}