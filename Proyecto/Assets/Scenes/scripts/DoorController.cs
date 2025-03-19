using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour, IActivable
{
    public Transform upperPart; // Parte superior de la puerta
    public Transform lowerPart; // Parte inferior de la puerta
    public float openDistance = 5f; // Distancia que se moverán
    public float openSpeed = 2f; // Velocidad de apertura

    private bool isOpening = false;
    private Vector3 upperClosedPos, lowerClosedPos;
    private Vector3 upperOpenPos, lowerOpenPos;

    void Start()
    {
        // Guardar posiciones iniciales
        upperClosedPos = upperPart.position;
        lowerClosedPos = lowerPart.position;

        // Calcular posiciones abiertas
        upperOpenPos = upperClosedPos + Vector3.up * openDistance;
        lowerOpenPos = lowerClosedPos + Vector3.down * openDistance;
    }

    public void Activate()
    {
        if (!isOpening)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        isOpening = true;
        float elapsedTime = 0;

        while (elapsedTime < 1f)
        {
            upperPart.position = Vector3.Lerp(upperClosedPos, upperOpenPos, elapsedTime * openSpeed);
            lowerPart.position = Vector3.Lerp(lowerClosedPos, lowerOpenPos, elapsedTime * openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar que lleguen a su destino exacto
        upperPart.position = upperOpenPos;
        lowerPart.position = lowerOpenPos;
    }

}
public interface IActivable
{
    void Activate();
}