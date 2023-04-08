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
    private bool isIdle;
    private bool isRunning;


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

        if (IsGrounded())
        {
            if (rb.velocity.x != 0f)
            {
                ChangeAnimationState("Run");
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
            }
            else
            {
                ChangeAnimationState("Idle");
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", true);
            }
        }
        else if (rb.velocity.y > 0f)
        {
            ChangeAnimationState("Jump");
        }
        else if (rb.velocity.y < 0f)
        {
            ChangeAnimationState("Fall");
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (PauseMenuScript.isPaused) return;
        if (context.performed && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);

        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.AddForce(Vector2.up * 0.5f, ForceMode2D.Impulse);

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

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

}
