using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float inputX;
    private bool isFacingRight = true;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float velPower;

    [Header("Grounded")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Jumping")]
    [SerializeField] private float jumpingPower;
    [SerializeField] private float jumpForce;

    Rigidbody2D rb;
    private Animator animator;
    public static bool isGrounded;
    private bool idle;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isFacingRight && rb.velocity.x > 0)
        {
            Flip();
        }
        else if (isFacingRight && rb.velocity.x < 0)
        {
            Flip();
        }

    }
    // Thinking about moving away from Rigidbody movement.
    private void FixedUpdate()
    {

        HandleJumping();

        HandleHorizontalMovement();

        float targetSpeed = inputX * moveSpeed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
        animator.SetFloat("y_vel", rb.velocity.y);

        if (IsGrounded())
        {
            animator.SetFloat("x_vel", Mathf.Abs(rb.velocity.x));
            animator.SetBool("isGrounded", isGrounded);



            if (rb.velocity.x != 0f)
            {
                idle = false;
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", idle);


            }
            else
            {
                idle = true;
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", idle);

            }
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    private void HandleJumping()
    {
        bool jumpPressed = InputManager.GetInstance().GetJumpPressed();
        if (PauseMenuScript.isPaused) return;
        if (isGrounded && jumpPressed)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
        }
    }

    private void HandleHorizontalMovement()
    {
        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        inputX = moveDirection.x;
    }

    // BoxCast to check if grounded using Raycast.
    public bool IsGrounded()
    {
        return isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    // Drawing the boxcast to get visuals for debugging.

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

}
