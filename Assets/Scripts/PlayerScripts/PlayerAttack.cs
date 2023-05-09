using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    public static bool interactPressed = false;

    public void Attack(InputAction.CallbackContext context)
    {
        if (PauseMenuScript.isPaused) return;
        if (context.performed && PlayerMovement.isGrounded && Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");
            nextAttackTime = Time.time + 1f / attackRate;
        }
        else if (context.performed && !PlayerMovement.isGrounded && Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack2");
            nextAttackTime = Time.time + 1f / attackRate;
        }

    }


}
