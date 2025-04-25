using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [Header("Configuración de Rotación")]
    [Tooltip("Eje de rotación del planeta (ej: Y para rotación vertical)")]
    public Vector3 rotationAxis = Vector3.up;

    [Tooltip("Velocidad de rotación en grados por segundo")]
    public float rotationSpeed = 10f;

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
