using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    public bool gameOver = false;
    private int jumpCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < 2))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpCount++;
            if (jumpCount == 1)
            {
                AudioManager.Instance.PlaySFX("Jump");
            }
            else if (jumpCount == 2)
            {
                AudioManager.Instance.PlaySFX("JetPack");
            }
        }
    }
    void OnCollisionStay()
    {
        isGrounded = true;
        jumpCount = 0;
    }
}
