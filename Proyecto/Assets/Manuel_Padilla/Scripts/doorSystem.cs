using UnityEngine;

public class doorSystem : MonoBehaviour
{
    [SerializeField] private float rotationTime = 5f;
    [SerializeField] private float doorOpened = 90f;
    [SerializeField] private float doorClosed = 0f;

    private Quaternion initialRotation;
    private Quaternion finalRotation;
    private float elapsedTime = 0f;
    [SerializeField] private bool doorState = false;    //False es que esta cerrado y true abierto

    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        initialRotation = transform.rotation;
        finalRotation = Quaternion.Euler(0, doorOpened, 0);
    }

    //Abre y cierra la puerta (funcion de interaccion)
    public void iOpenTheDoor()
    {
        if(elapsedTime < rotationTime)
        {
            elapsedTime = Time.deltaTime;

            float completedPercent = elapsedTime / rotationTime;

            if(doorState)
            {
                transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, completedPercent);
                doorState = false;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(finalRotation, initialRotation, completedPercent);
                doorState = true;
            }
        }
        else
        {
            elapsedTime = 0f;
        }
    }
}
