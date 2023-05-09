using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void Awake()
    {
        playerInRange = false;
    }

    private void FixedUpdate()
    {
        if (playerInRange)
        {
            if (InputManager.GetInstance().GetInteractPressed())
            {
                Debug.Log(inkJSON.text);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Players")
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Players")
        {
            playerInRange = false;

        }
    }
}
