using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private float spawnDelay = 2;
    private float spawnInterval = 1.5f;
    private int maxEnemies = 15; // Ensure only one enemy at a time
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();

    private CapsuleController capsuleControllerScript;

    void Start()
    {
        // Get all child transforms (spawn points)
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points found as children of the SpawnManagerX");
        }

        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        capsuleControllerScript = GameObject.Find("Capsule").GetComponent<CapsuleController>();
    }

    void SpawnObjects()
    {
        if (spawnedEnemies.Count < maxEnemies)
        {
            Vector3 spawnLocation = GetRandomSpawnPoint();
            int index = Random.Range(0, objectPrefabs.Length);

            GameObject spawnedEnemy = Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
            spawnedEnemies.Add(spawnedEnemy);

            CleanupDestroyedEnemies();
        }
    }

    Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex].position;
    }

    void CleanupDestroyedEnemies()
    {
        spawnedEnemies.RemoveAll(item => item == null);
    }


}
