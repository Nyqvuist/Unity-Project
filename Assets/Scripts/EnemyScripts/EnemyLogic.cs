using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField] private Animator animator;
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

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
