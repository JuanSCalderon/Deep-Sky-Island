using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public GameOver gameOverScript;
    public float floatForce;
    public float gravityModifier = 1.5f;
    private Rigidbody playerRb;
    public Vector3 originalGravity;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip candySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;
    public bool isLowEnough;



    // Start is called before the first frame update
    void Start()
    {
        originalGravity = Physics.gravity;
    Physics.gravity *= gravityModifier;
    playerAudio = GetComponent<AudioSource>();
    playerRb = GetComponent<Rigidbody>();
    playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space)&& isLowEnough && !gameOver )
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        if (transform.position.y > 12)
        {
            isLowEnough = false;
        }
        else
        {
            isLowEnough = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with salt, explode and set gameOver to true
        if (other.gameObject.CompareTag("Salt"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            gameOverScript.ShowGameOver();
        }

        // if player collides with candy, fireworks
        else if (other.gameObject.CompareTag("Candy"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(candySound, 1.0f);
            HUD.candyTotal++;
            Destroy(other.gameObject);

        }

        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }

    }

}
