using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GetHit()
    {
        animator.SetTrigger("Hurt");
    }
}
