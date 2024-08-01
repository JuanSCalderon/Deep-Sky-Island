using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    public float detectionRadius = 10f;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    public AudioClip enemySound;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("Player (Capsule) not found in the scene.");
        }

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy.");
        }

        // Reproduce el sonido del enemigo en loop
        // AudioManager.Instance.PlaySFXLoop("Enemy");

        // Configura el AudioSource para reproducir el sonido en loop
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = enemySound;
        audioSource.loop = true;
        audioSource.volume = 1f;
        audioSource.pitch = 1.5f;
        audioSource.Play();


        if (player == null)
        {
            Debug.LogError("Player (Capsule) not found in the scene.");
        }
    }

    void Update()
    {
        if (player != null && agent != null)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= detectionRadius)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detener el sonido SFX cuando el enemigo colisiona con el jugador
            audioSource.Stop();
            Debug.Log(gameObject.name);
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        player = GameObject.Find("Capsule").transform;
    }
}
