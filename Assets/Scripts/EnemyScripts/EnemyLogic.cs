using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField] private Animator animator;

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    // Retrieve Game Objects of Player and Enemy
    private GameObject playerObj = null;
    private GameObject enemyObj = null;

    // Vector3 position of player and enemy
    Vector3 playerPos;
    Vector3 enemyPos;

    private Rigidbody2D rb;
    public static bool isGrounded;
    private bool idle;

    private float attackRange = 1.5f;
    private bool isChasing;
    private float chaseDistance = 7;
    private float chaseOutofDistance = 15;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Instantiate gameObject for the player and enemy, depends on the name of the object
        if (playerObj == null)
            playerObj = GameObject.Find("Player");

        if (enemyObj == null)
            enemyObj = GameObject.Find("Enemy");

    }

    private void Update()
    {
        // Retrieve the x and y positon of player and enemy object
        Vector3 playerPos = playerObj.transform.position;
        Vector3 enemyPos = enemyObj.transform.position;

        // Use this to print out position of characters
        //Debug.Log("playerPos: " + playerPos);
        //Debug.Log("enemyPos: " + enemyPos);

        // Chase Distance is when to start chasing
        if (Vector2.Distance(enemyPos, playerPos) < chaseDistance)
        {
            isChasing = true;
        }

        // Out Of Distance is when to stop chasing
        if (Vector2.Distance(enemyPos, playerPos) > chaseOutofDistance)
        {
            isChasing = false;
        }

        // Run towards player if in chase mode, or else idle
        // If player is left side of enemy, face left, else face right. Same for running direction.
        // Vector3 Constructor takes in three number variables (x,y,z) (x and y are size?)
        if (isChasing)
        {
            if (playerPos.x < enemyPos.x)
            {
                enemyObj.transform.localScale = new Vector3(7.441683f, 7.905868f, 2);
                animator.SetBool("isRunning", true);
                rb.velocity = new Vector2(-4, 0);
            }
            else if (playerPos.x > enemyPos.x)
            {
                enemyObj.transform.localScale = new Vector3(-7.441683f, 7.905868f, 2);
                animator.SetBool("isRunning", true);
                rb.velocity = new Vector2(4, 0);
            }

        }
        else
        {

            animator.SetBool("isRunning", false);

        }


    }

    // I tried to stop the player when they get hit or to stall them but.. no luck
    public void GetHit()
    {
        rb.velocity = new Vector2(0, 0);
        animator.SetTrigger("Hurt");
        audioSource.PlayOneShot(PlayRandomSound());
    }

    //IEnumerator GetHitCoroutine()
    //{
    //    rb.velocity = new Vector2(0, 0);
    //    animator.SetTrigger("Hurt");
    //    audioSource.PlayOneShot(PlayRandomSound());
    //    yield return new WaitForSeconds(3);
    //}

    AudioClip PlayRandomSound()
    {
        return audioClipArray[Random.Range(0, audioClipArray.Length)];
    }
}
