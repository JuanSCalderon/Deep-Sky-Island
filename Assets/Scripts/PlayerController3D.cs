using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController3D : MonoBehaviour
{
    CharacterController _playerController;

    [SerializeField] float _playerSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float hitPoints = 3;
    private readonly int fuel = 5; // Número de elementos necesarios para activar el jetpack
    private readonly float gravity = 9.8f;
    [SerializeField] private int collectedItems = 0;
    //private readonly float sphereRadius = 0.45f;
    public Text energyText; // UI Text para mostrar la energía restante
    public Text itemsText; // UI Text para mostrar el número de items recogidos

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

        Vector3 movementInput = new(vInput, 0, -hInput);
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
            _playerController.Move(velocity * Time.deltaTime);
        }
    }
    private bool GroundCheck()
    {
        Ray ray = new(groundCheck.position, Vector3.down);
        float rayLength = 0.5f;
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        return Physics.Raycast(ray, out RaycastHit hit, rayLength, groundMask);

        //return Physics.SphereCast(ray, sphereRadius, 0, groundMask);

    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collectedItems++;
            Destroy(collision.gameObject);
            

            if (collectedItems >= fuel)
            {
                Debug.Log("Jetpack listo para usar");
            }
        }
    }

    //void UpdateItemsText()
    //{
    //    itemsText.text = "Items: " + collectedItems;
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(groundCheck.position, .45f);
    //}
}




