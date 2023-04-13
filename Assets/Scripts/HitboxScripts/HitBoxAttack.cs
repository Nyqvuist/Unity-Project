using UnityEngine;

public class HitBoxAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyLogic>().GetHit();
        }

    }
}
