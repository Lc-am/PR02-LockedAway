using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footstepSound;
    public float walkInterval = 0.5f;
    public float runInterval = 0.3f;

    private AudioSource audioSource;
    private float stepTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentInterval = isRunning ? runInterval : walkInterval;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                audioSource.PlayOneShot(footstepSound);
                stepTimer = currentInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
