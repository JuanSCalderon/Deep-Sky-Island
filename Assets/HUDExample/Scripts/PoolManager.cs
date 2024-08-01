using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] List<GameObject> pooledEnemies = new List<GameObject>();
    public int waveNumber = 3;
    public int activeCount;

    private CapsuleController capsuleControllerScript;

    void Start()
    {
        capsuleControllerScript = GameObject.Find("Capsule").GetComponent<CapsuleController>();
        InstancePool(10); // Adjust the pool size as needed
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        activeCount = CountActiveEnemies();

        if (activeCount < 3)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void InstancePool(int numberEnemies)
    {
        for (int i = 0; i < numberEnemies; i++)
        {
            foreach (var prefab in enemyPrefabs)
            {
                GameObject spawnEnemy = Instantiate(prefab);
                pooledEnemies.Add(spawnEnemy);
                spawnEnemy.SetActive(false);
                spawnEnemy.transform.SetParent(transform);
            }
        }
    }

    GameObject CallEnemy()
    {
        foreach (var enemy in pooledEnemies)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null;
    }

    int CountActiveEnemies()
    {
        activeCount = 0;
        foreach (var enemy in pooledEnemies)
        {
            if (enemy.activeInHierarchy)
            {
                activeCount++;
            }
        }
        return activeCount;
    }

    void SpawnEnemyWave(int numberEnemies)
    {
        for (int i = 0; i < numberEnemies; i++)
        {
            GameObject selectedEnemy = CallEnemy();

            if (selectedEnemy != null)
            {
                Vector3 spawnLocation = GetRandomPointNearPlayer();
                selectedEnemy.transform.position = spawnLocation;
                selectedEnemy.SetActive(true);
            }
        }
    }

    Vector3 GetRandomPointNearPlayer()
    {
        Transform playerTransform = capsuleControllerScript.transform;
        float spawnRadius = 10f; // Adjust this value as needed
        Vector3 randomPoint = Random.insideUnitSphere * spawnRadius;
        randomPoint += playerTransform.position;
        randomPoint.y = playerTransform.position.y; // Keep the same height as the player

        return randomPoint;
    }
}
