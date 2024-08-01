using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Animator Animator;
    private float horizontalMove;
    private float verticalMove;
    private Vector3 playerInput;
    public CharacterController player;
    public float playerSpeed;
    private Vector3 movePlayer;
    public float gravity = 9.81f;
    public float fallVelocity;
    public float jumpForce;
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    void Start()
    {
        Animator = GetComponent<Animator>();
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        Animator.SetBool("running", horizontalMove != 0.0f || verticalMove != 0.0f);


        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = movePlayer * playerSpeed;
        player.transform.LookAt(player.transform.position + movePlayer);

        setGravity();

        PlayerSkills();
        player.Move(movePlayer * Time.deltaTime);
    }

    void camDirection()
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
            // Animator.SetBool("jumping", true);
        }
        else if (player.isGrounded)
        {
            // Animator.SetBool("jumping", false);
        }
    }
    void setGravity()
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
    }}