using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField] private Animator animator;
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    // Start is called before the first frame update
    void Start()
    {

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
