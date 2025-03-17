using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform upperPart; // Parte superior de la puerta
    public Transform lowerPart; // Parte inferior de la puerta

    public Vector3 upperOpenOffset; // Distancia a mover la parte superior
    public Vector3 lowerOpenOffset; // Distancia a mover la parte inferior

    public float openSpeed = 2f;
    private bool isOpen = false;

    private Vector3 upperClosedPos;
    private Vector3 lowerClosedPos;

    void Start()
    {
        upperClosedPos = upperPart.position;
        lowerClosedPos = lowerPart.position;
    }

    public void ToggleDoor()
    {
        if (!isOpen)
            StartCoroutine(OpenDoor());
        else
            StartCoroutine(CloseDoor());
    }

    IEnumerator OpenDoor()
    {
        isOpen = true;
        float elapsedTime = 0;
        while (elapsedTime < openSpeed)
        {
            upperPart.position = Vector3.Lerp(upperClosedPos, upperClosedPos + upperOpenOffset, elapsedTime / openSpeed);
            lowerPart.position = Vector3.Lerp(lowerClosedPos, lowerClosedPos + lowerOpenOffset, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        upperPart.position = upperClosedPos + upperOpenOffset;
        lowerPart.position = lowerClosedPos + lowerOpenOffset;
    }

    IEnumerator CloseDoor()
    {
        isOpen = false;
        float elapsedTime = 0;
        while (elapsedTime < openSpeed)
        {
            upperPart.position = Vector3.Lerp(upperClosedPos + upperOpenOffset, upperClosedPos, elapsedTime / openSpeed);
            lowerPart.position = Vector3.Lerp(lowerClosedPos + lowerOpenOffset, lowerClosedPos, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        upperPart.position = upperClosedPos;
        lowerPart.position = lowerClosedPos;
    }
}
