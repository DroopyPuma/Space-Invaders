using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private int scoreValue;
    //
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    //
    public float speed = 50.0f;
    //
    public float maxLifetime = 30f;
    //
    private Rigidbody rb;

    public GameObject player;

    // refercnes the ScoreMangerScript for the score to work
    [SerializeField]
    private ScoreManager scoreManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    private void Start()
    {
        
        // randomizes the scale of the asteroids
        this.transform.localScale = Vector3.one * this.size;
        // randomizes the rotation 
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        // matches the mass to the size 
        rb.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //on collision to bullet creat two asteroids and delete the object
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.LogWarning("collided with Bullet");
            if ((this.size * 0.5f ) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<ScoreManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }
    //create a split of the asteroid and have it be the smallest size
    private void CreateSplit()
    { 
        Vector2 positition = this.transform.position;
        positition += Random.insideUnitCircle * 0.5f;

        AsteroidController half = Instantiate(this, positition, this.transform.rotation);
        half.size = this.size * minSize;
        half.SetTrajectory(Random.insideUnitCircle.normalized);

    }

    public int GetScoreValue()
    {
        return scoreValue;

    }
}
