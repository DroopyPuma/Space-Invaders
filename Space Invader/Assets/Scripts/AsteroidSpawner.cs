using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public AsteroidController asteroidPrefab;

    [SerializeField]
    private float trajectoryVariance = 15.0f;
    //
    [SerializeField]
    private float spawnRate = 2.0f;
    [SerializeField]
    private int spawnAmount = 1;
    [SerializeField]
    private float spawnDistance = 15f;

    //public AsteroidController asteroid;

    private void Start()
    {
        //
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {

            // spawns a asteroid in a random loction within a circle around the spawnpoint. 
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            // settinf the trajectory to have a variance of 15 degees  
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            //giving them a random rotation and size on spawn.
            AsteroidController asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
