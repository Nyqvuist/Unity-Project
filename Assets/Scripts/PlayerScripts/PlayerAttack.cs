using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;

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

    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null) return;

    //    Gizmos.DrawSphere(attackPoint.position, attackRange);
    //}

    //private void AttackLogic(string attack)
    //{
    //    animator.SetTrigger(attack);
    //    nextAttackTime = Time.time + 1f / attackRate;
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        enemy.GetComponent<EnemyLogic>().GetHit();
    //    }
    //}
}
