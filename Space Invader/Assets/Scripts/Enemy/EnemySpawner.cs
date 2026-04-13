using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectPrefabs;
    private float timeUntilObjectSpawn;
    public float objectSpawnTime;
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

        //GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        GameObject spawnedObject = Instantiate(objectToSpawn, new Vector3(Random.Range(-10f, 10f),20f,0), Quaternion.identity);

        //  RANDOMIZING THE TIMER 
        objectSpawnTime = Random.Range(1f, 3f);
    }



}
