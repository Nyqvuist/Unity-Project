using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static bool isAttacking;
    
    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (PauseMenuScript.isPaused) return;
        if (context.performed && PlayerMovement.isGrounded)
        {
            animator.SetTrigger("Attack");
        } else if (context.performed && !PlayerMovement.isGrounded)
        {
            animator.SetTrigger("Attack2");
            isAttacking = true;
        } else if (context.canceled)
        {
            isAttacking = false;
        }

    }
}
