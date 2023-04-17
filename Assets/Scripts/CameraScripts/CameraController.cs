using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float offset;
    [SerializeField] private float offsetSmoothing;
    private Vector3 playerPosition;

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        //Not sure how i feel about this type of camera.

        //if (player.transform.localScale.x > 0f)
        //{
        //    playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);

        //}
        //else
        //{
        //    playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        //}

        //transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
