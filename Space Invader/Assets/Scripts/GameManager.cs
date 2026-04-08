using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int lives = 3;
    public float respawnTime = 0.0f;
    public float respawnInvulerabilityTime = 3.0f;

    [SerializeField]
    private TextMeshProUGUI livesNumberText;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    // removes a life and gameovers if at 0
    public void PlayerDied()
    {
        this.lives--;
        livesNumberText.text = lives.ToString();

        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            //respawnTime -= Time.deltaTime;
            Invoke(nameof(Respawn),this.respawnTime);
        }

    }
    //respawns th player Zero and make them invicible for a time 
    public void Respawn()
    { 
        
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollision), this.respawnInvulerabilityTime);
        Debug.Log("respawn was invoked but did'nt happen");

    }

    public float GetLives()
    {
        return lives; 
    }
    //makes the player invincable
    private void TurnOnCollision()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    //sets the lives back to 3 and score back to 0
    public void GameOver()
    {
        this.lives = 3;
        //this.score = 0;

        Invoke(nameof(Respawn), this.respawnTime);
    }
}
