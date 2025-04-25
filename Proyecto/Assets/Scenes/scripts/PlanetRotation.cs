using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [Header("Configuraci�n de Rotaci�n")]
    [Tooltip("Eje de rotaci�n del planeta (ej: Y para rotaci�n vertical)")]
    public Vector3 rotationAxis = Vector3.up;

    [Tooltip("Velocidad de rotaci�n en grados por segundo")]
    public float rotationSpeed = 10f;

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
