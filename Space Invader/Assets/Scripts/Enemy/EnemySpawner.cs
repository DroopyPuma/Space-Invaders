using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemySpawner : MonoBehaviour
{
    // Array of enemy prefabs to randomly choose from
    [SerializeField] private GameObject[] objectPrefabs;

    // Timer that counts up until we spawn
    private float timeUntilObjectSpawn;

    // How often enemies should spawn
    public float objectSpawnTime = 2f;

    // (Optional) speed value for spawned objects
    public float objectSpeed;

    private void Update()
    {
        // Run the spawn timer every frame
        spawnLoop();
    }

    private void spawnLoop()
    {
        // Add time every frame
        timeUntilObjectSpawn += Time.deltaTime;

        // When timer reaches spawn time
        if (timeUntilObjectSpawn >= objectSpawnTime)
        {
            // Spawn an enemy
            SpawnEnemy();

            // Reset timer
            timeUntilObjectSpawn = 0f;
        }
    }

    private void SpawnEnemy()
    {
        // Pick a random prefab from the array
        GameObject objectToSpawn = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

        // Create random X position
        float randomX = Random.Range(-10f, 10f);

        // Keep Y and Z the same as spawner
        Vector3 spawnPosition = new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z );

        // Spawn enemy at random position
        GameObject spawnedObject = Instantiate(
            objectToSpawn,          // prefab to spawn
            spawnPosition,          // random position
            Quaternion.identity);   // no rotation


        // Print to console to confirm spawn
        //Debug.Log("Object spawned: " + spawnedObject.name);
    }
}

