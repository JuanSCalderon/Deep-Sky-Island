using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    CharacterController _playerController;

    [SerializeField] float _playerSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float jumpForce;
    private readonly float gravity = 9.8f;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    private Vector3 velocity;
    [SerializeField] private bool isGrounded;

    void Start()
    {
        _playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = GroundCheck();

        if (isGrounded)
        {
            velocity.y = 0f;
            Jump();           
        }
        
        Movement();

        if (!isGrounded)
        {
            velocity.y -= gravity * 2f * Time.deltaTime;
            _playerController.Move(velocity * Time.deltaTime);
        }

        
    }

    private void Movement()
    {

        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 movementInput = new(hInput, 0, vInput);
        Vector3 movementDirection = movementInput.normalized;


        if (movementInput != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }

        _playerController.Move(_playerSpeed * Time.deltaTime * movementDirection);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * gravity);            
        }
        _playerController.Move(velocity * Time.deltaTime);
    }
    private bool GroundCheck()
    {
        float rayLength = 0.45f;
        Ray ray = new(groundCheck.position, Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        return Physics.Raycast(ray, out RaycastHit hit, rayLength, groundMask);
    }
}




