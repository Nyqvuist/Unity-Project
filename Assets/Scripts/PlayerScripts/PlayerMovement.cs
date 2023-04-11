using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Jumping")]
    [SerializeField] private float jumpingPower;
    [SerializeField] private float jumpForce;

    Rigidbody2D rb;
    private Animator animator;
    private string currentState;
    public static bool isGrounded;


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

    private void FixedUpdate()
    {
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
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", true);
            }
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (PauseMenuScript.isPaused) return;
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);

        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    public bool IsGrounded()
    {
        return isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

}
