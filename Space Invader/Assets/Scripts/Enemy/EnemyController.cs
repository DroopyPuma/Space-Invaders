using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // How many points this enemy is worth
    [SerializeField]
    private int scoreValue = 100;

    // How long before the enemy auto-destroys
    public float maxLifetime = 30f;

    // Rigidbody reference
    private Rigidbody rb;

    // Reference to player (optional if you plan to track player)
    public GameObject player;

    // References the ScoreManager script
    [SerializeField]
    private ScoreManager scoreManager;

    private void Awake()
    {
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Find ScoreManager in scene using tag
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    private void Start()
    {
        // Randomize Z rotation
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        // OPTIONAL: set mass based on scale instead of missing "size"
        rb.mass = transform.localScale.x;
    }
    private void OnEnable()
    {
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If hit by bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Add score
            scoreManager.EnemyDestroyed(this);

            // Destroy enemy
            Destroy(gameObject);
        }
    }

    public int GetScoreValue()
    {
        return scoreValue;
    }
}
