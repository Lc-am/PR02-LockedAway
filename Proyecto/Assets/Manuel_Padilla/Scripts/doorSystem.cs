using UnityEngine;

public class doorSystem : MonoBehaviour
{
    private bool activated = false;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void iOpenTheDoor()
    {
        if(!activated)
        {
            activated = true;

            rb.transform.rotation = Quaternion.Euler(0, 0, transform.position.z * -90);
        }
    }
}
