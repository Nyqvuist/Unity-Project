using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Players")
        {
            playerInRange = true;
            Debug.Log("Player Entered.");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Players")
        {
            playerInRange = false;
            Debug.Log("Player Exited.");
        }
    }
}
