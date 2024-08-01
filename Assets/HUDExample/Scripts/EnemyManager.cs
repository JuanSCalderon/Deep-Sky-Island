using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo sin NavMeshAgent
    private float spawnDelay = 2;
    private int maxEnemies = 5; // Número máximo de enemigos a la vez
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private NavMeshTriangulation navMeshTriangulation;
    private CapsuleController capsuleControllerScript;

    void Start()
    {
        navMeshTriangulation = NavMesh.CalculateTriangulation();
        if (navMeshTriangulation.vertices.Length == 0)
        {
            Debug.LogError("NavMesh triangulation has no vertices. Make sure your NavMesh is baked.");
        }

        capsuleControllerScript = GameObject.Find("Capsule")?.GetComponent<CapsuleController>();

        if (capsuleControllerScript == null)
        {
            Debug.LogError("CapsuleController script not found. Make sure there is a GameObject named 'Capsule' with a CapsuleController script attached.");
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not assigned. Please assign an enemy prefab in the inspector.");
        }

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (!capsuleControllerScript.gameOver && spawnedEnemies.Count < maxEnemies)
        {
            Vector3 spawnLocation = GetRandomPointOnNavMesh();
            if (spawnLocation != Vector3.zero) // Asegúrate de que la posición es válida
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
                NavMeshAgent agent = enemy.AddComponent<NavMeshAgent>();
                agent.speed = 3.5f; // Configura la velocidad u otras propiedades según sea necesario
                spawnedEnemies.Add(enemy);

                CleanupDestroyedEnemies();
            }
        }
    }

    public void OnEnemyDestroyed(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        CleanupDestroyedEnemies();
        SpawnEnemy();
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        int attempts = 0;
        while (attempts < 30) // Intenta encontrar una posición válida en el NavMesh
        {
            int triangleIndex = Random.Range(0, navMeshTriangulation.indices.Length / 3) * 3;
            Vector3 vertex1 = navMeshTriangulation.vertices[navMeshTriangulation.indices[triangleIndex]];
            Vector3 vertex2 = navMeshTriangulation.vertices[navMeshTriangulation.indices[triangleIndex + 1]];
            Vector3 vertex3 = navMeshTriangulation.vertices[navMeshTriangulation.indices[triangleIndex + 2]];

            Vector3 randomPoint = RandomPointInTriangle(vertex1, vertex2, vertex3);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position; // Devuelve una posición válida en el NavMesh
            }
            attempts++;
        }
        Debug.LogWarning("Failed to find a valid point on NavMesh after 30 attempts.");
        return Vector3.zero; // Devuelve Vector3.zero si no se encuentra una posición válida
    }

    Vector3 RandomPointInTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        float u = Random.value;
        float v = Random.value;
        if (u + v > 1)
        {
            u = 1 - u;
            v = 1 - v;
        }
        return v1 + u * (v2 - v1) + v * (v3 - v1);
    }

    void CleanupDestroyedEnemies()
    {
        spawnedEnemies.RemoveAll(item => item == null);
    }
}
