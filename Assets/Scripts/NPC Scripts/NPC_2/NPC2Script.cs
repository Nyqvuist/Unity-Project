using UnityEngine;

public class NPC2Script : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Retrieve Game Objects of Player and Enemy
    private GameObject playerObj = null;
    private GameObject npcObj = null;

    // Vector3 position of player and enemy
    Vector3 playerPos;
    Vector3 npcPos;

    // NPC scale X and Y 
    float npcScaleX;
    float npcScaleY;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Instantiate gameObject for the player and NPC, depends on the name of the object tag
        if (playerObj == null)
            playerObj = GameObject.Find("Player");

        if (npcObj == null)
            npcObj = GameObject.Find("NPC_2");

        // Obtaining the X and Y scale of current NPC object
        npcScaleX = npcObj.transform.localScale.x;
        npcScaleY = npcObj.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve X and Y position of Player and NPC Object
        Vector3 playerPos = playerObj.transform.position;
        Vector3 npcPos = npcObj.transform.position;

        // Using scale variables gained in Start, can dynamically obtain X and Y scale
        // of different NPC objects
        if (playerPos.x < npcPos.x)
        {
            npcObj.transform.localScale = new Vector3(-(npcScaleX), npcScaleY, 2);
        }
        else if (playerPos.x > npcPos.x)
        {
            npcObj.transform.localScale = new Vector3(npcScaleX, npcScaleY, 2);
        }
    }
}
