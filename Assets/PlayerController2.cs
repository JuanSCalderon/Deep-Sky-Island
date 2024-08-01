using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    private Animator Animator;

    private float horizontalMove;
    private float verticalMove;

    private Vector3 playerInput;
    private Vector3 movePlayer;
    private Vector3 camForward;
    private Vector3 camRight;

    public CharacterController player;
    public Camera mainCamera;

    public float playerSpeed;
    public float gravity = 9.81f;
    public float fallVelocity;
    public float jumpForce;

    [SerializeField] int hitPoints = 3;
    [SerializeField] float fuelCanisters = 0; // N�mero de elementos necesarios para activar el jetpack
    [SerializeField] private FuelBar fuelBar;
    [SerializeField] private GameObject[] life;
    private readonly float maxFuel = 5f;

    bool hasJetPack;
    Transform jetParticles;


    void Start()
    {
        Animator = GetComponent<Animator>();
        player = GetComponent<CharacterController>();
        fuelBar.SetFuel(fuelCanisters);
        jetParticles = transform.GetChild(2);
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        Animator.SetBool("running", horizontalMove != 0.0f || verticalMove != 0.0f);


        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer *= playerSpeed;
        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();
        player.Move(movePlayer * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        ActivateJetpack();
        fuelBar.SetFuel(fuelCanisters);
        if (hasJetPack) StartCoroutine(DeactivateJetPack());
    }

    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;

            if (jumpForce == 30)
            {
                AudioManager.Instance.PlaySFX("JetPack");
            }
            else if (jumpForce == 7)
            {
                AudioManager.Instance.PlaySFX("Jump");
            }
        }

    }
    void SetGravity()
    {

        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;

        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            if (fuelCanisters < maxFuel) fuelCanisters++;
            AudioManager.Instance.PlaySFX("PowerUp");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hitPoints > 0) hitPoints--;
            life[hitPoints].SetActive(false);
            Debug.Log($"hit points: {hitPoints}");
            AudioManager.Instance.PlaySFX("EnemyCrash");
            Destroy(collision.gameObject);
            if (hitPoints == 0) KillPlayer();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            GameManager.Instance.ReloadScene();
        }
        else if (collision.gameObject.CompareTag("Spaceship"))
        {
            GameManager.Instance.LoadVictoryScene();
        }
    }

    private void KillPlayer()
    {
        GameManager.Instance.LoadGameOverScene();
        AudioManager.Instance.StopAndPlayMusic("GameOverMusic");
    }

    private void ActivateJetpack()
    {
        if (fuelCanisters < maxFuel) return;
        jumpForce = 30;
        jetParticles.gameObject.SetActive(true);
        hasJetPack = true;

    }

    private IEnumerator DeactivateJetPack()
    {
        hasJetPack = false;
        while (fuelCanisters > 0)
        {
            yield return new WaitForEndOfFrame();
            fuelCanisters = fuelCanisters > 0 ? fuelCanisters - (0.50f * Time.deltaTime) : 0;
        }

        jumpForce = 7;
        jetParticles.gameObject.SetActive(false);
    }
}