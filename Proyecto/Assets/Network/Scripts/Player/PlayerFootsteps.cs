using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footstepSound;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlayFootstep()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = footstepSound;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) // ejemplo: al moverse hacia adelante
        {
            PlayFootstep();
        }
    }
}
