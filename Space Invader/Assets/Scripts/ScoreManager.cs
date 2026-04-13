using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Win32.SafeHandles;

public class ScoreManager : MonoBehaviour
{
    
    public float score = 0;

    [SerializeField]
    private TextMeshProUGUI scoreManagerText;



    public float GetScore()
    { 
        return score;
    }

    public void AsteroidDestroyed(EnemyController asteroid)
    {
        if (asteroid.size < .75f)
        {
            this.score += 3.0f;
        }
        else if (asteroid.size < 1.0f)
        {
            this.score += 2.0f;
        }
        else
        {
            this.score += 1.0f;
        }
        AddScore(this.score);
    }


    public void AddScore(float newScore)
    { 
        score = score + newScore;
        Debug.Log("the current score is:" + score);
        scoreManagerText.text = score.ToString();
    }

    public void SetScore(float newScore)
    { 
        score = newScore;
    }
}