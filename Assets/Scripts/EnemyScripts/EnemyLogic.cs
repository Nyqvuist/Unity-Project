using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField] private Animator animator;

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    // Retrieve Game Objects of Player and Enemy
    public GameObject playerObj = null;
    public GameObject enemyObj = null;

    // Vector3 position of player and enemy
    Vector3 playerPos;
    Vector3 enemyPos;

    private void Start()
    {
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

        // If player is left side of enemy, face right, else face left
        // Vector3 Constructor takes in three number variables (x,y,z)
        if (playerPos.x < enemyPos.x)
        {
            enemyObj.transform.localScale = new Vector3(2.441683f, 2.905868f, 2);
        }
        else
        {
            enemyObj.transform.localScale = new Vector3(-2.441683f, 2.905868f, 2);
        }
    }

    public void GetHit()
    {
        animator.SetTrigger("Hurt");
        audioSource.PlayOneShot(PlayRandomSound());
    }

    AudioClip PlayRandomSound()
    {
        return audioClipArray[Random.Range(0, audioClipArray.Length)];
    }
}
