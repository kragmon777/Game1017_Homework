using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] public float speed = 5.0f;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;

    [Tooltip("This represents the lowest position the player can go in the axis before dying.")]
    [SerializeField] private float lowerYLimit;

    private Vector3 startPosition;
    private Rigidbody2D rb;

    private bool jumpPressed = false;
    private bool isGrounded = false;
    
    public void Initialize()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) return;
        CheckGrounded();

        if (transform.position.y < lowerYLimit)
        {
            GameManager.Instance.GameOver();
        }
    }   

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) return;

        if (rb.linearVelocity.x <= speed)
        {
            rb.AddForceX(speed);
        }

        if (jumpPressed && isGrounded)
        {
            Jump();
        }

        jumpPressed = false;
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void Jump()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0;
        rb.linearVelocity = velocity;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
 
    public void OnJump()
    {
        jumpPressed = true;
    }

    public void Reset()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;

        rb.simulated = false;
        jumpPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Obstacle>())
        {
            GameManager.Instance.GameOver();
        }
    }
}
