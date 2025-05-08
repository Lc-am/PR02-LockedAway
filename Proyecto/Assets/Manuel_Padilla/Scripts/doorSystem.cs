using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class doorSystem : NetworkBehaviour, IInteractable
{
    [SerializeField] private float rotationTime = 5f;   // Tiempo de transición
    [SerializeField] private float doorOpened = 90f;    // Ángulo de puerta abierta
    [SerializeField] private float doorClosed = 0f;     // Ángulo de puerta cerrada
    [SerializeField] public bool canInteract;  //Para saber si es parte de un puzzle o una puerta interactuable

    private Quaternion initialRotation;   // Rotación inicial (cerrada)
    private Quaternion finalRotation;     // Rotación final (abierta)
    private float elapsedTime = 0f;       // Tiempo transcurrido durante la transición
    private bool doorState = false;       // Si la puerta está cerrada (false) o abierta (true)
    private bool isAnimating = false;     // Para saber si la puerta está en proceso de abrir o cerrar

    private void Awake()
    {
        // Inicializar las rotaciones
        initialRotation = transform.rotation;
        finalRotation = Quaternion.Euler(0, doorOpened, 0); // Rotación cuando la puerta está 
    }

    // Función para abrir o cerrar la puerta con transición
    public void iOpenTheDoor()
    {
        Debug.Log("opening");
        // Si la puerta no está animándose, entonces comenzamos una animación
        if (!isAnimating)
        {
            isAnimating = true;  // Iniciar animación
            elapsedTime = 0f;    // Reiniciar el tiempo transcurrido
        }
    }

    private void Update()
    {
        // Solo realizar la animación si estamos animando
        if (isAnimating)
        {
            // Acumulamos el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Calculamos el porcentaje completado de la animación
            float completedPercent = elapsedTime / rotationTime;

            // Si la puerta está cerrada, la abrimos
            if (!doorState)
            {
                transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, completedPercent);
            }
            // Si la puerta está abierta, la cerramos
            else
            {
                transform.rotation = Quaternion.Lerp(finalRotation, initialRotation, completedPercent);
            }

            // Si la animación ha terminado
            if (elapsedTime >= rotationTime)
            {
                isAnimating = false;  // Deja de animar
                doorState = !doorState;  // Cambia el estado de la puerta (abierta o cerrada)
            }
        }
    }

    void IInteractable.StartInteraction()
    {
        Debug.Log("interacted");
        if(canInteract)
        {
            isAnimating = true;
        }
    }
}
