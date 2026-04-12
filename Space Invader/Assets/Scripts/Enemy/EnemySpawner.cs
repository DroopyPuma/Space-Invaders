using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectPrefabs;
    private float timeUntilObjectSpawn;
    public float objectSpawnTime = 2f;
    public float objectSpeed;

    

    private void Update()
    {
        spawnLoop();
    }

    private void spawnLoop()
    {
        timeUntilObjectSpawn += Time.deltaTime;
        // Debug.Log("the time until platform spawn is: " + timeUntilPlatformSpawn);

        if (timeUntilObjectSpawn >= objectSpawnTime)
        {
            Spawn();
            // Debug.Log("the time until platform spawn is in the inside loop is: " + timeUntilPlatformSpawn);
            timeUntilObjectSpawn = 0f;
            // Debug.Log("the time until platform spawn is after the reset: " + timeUntilPlatformSpawn);
        }
    }

    private void Spawn()
    {
        GameObject objectToSpawn = ObjectPrefabs[Random.Range(0, ObjectPrefabs.Length)];
        Debug.Log("object To spawn " + objectToSpawn);

        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);

        //Rigidbody2D platformRB = spawnedPlatform.GetComponent<Rigidbody2D>();
        //platformRB.velocity = Vector2.down * platformSpeed;
    }



}
