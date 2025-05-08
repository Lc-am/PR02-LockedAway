using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class doorSystem : NetworkBehaviour, IInteractable
{
    [SerializeField] private float rotationTime = 5f;   // Tiempo de transici�n
    [SerializeField] private float doorOpened = 90f;    // �ngulo de puerta abierta
    [SerializeField] private float doorClosed = 0f;     // �ngulo de puerta cerrada
    [SerializeField] public bool canInteract;  //Para saber si es parte de un puzzle o una puerta interactuable

    private Quaternion initialRotation;   // Rotaci�n inicial (cerrada)
    private Quaternion finalRotation;     // Rotaci�n final (abierta)
    private float elapsedTime = 0f;       // Tiempo transcurrido durante la transici�n
    private bool doorState = false;       // Si la puerta est� cerrada (false) o abierta (true)
    private bool isAnimating = false;     // Para saber si la puerta est� en proceso de abrir o cerrar

    private void Awake()
    {
        // Inicializar las rotaciones
        initialRotation = transform.rotation;
        finalRotation = Quaternion.Euler(0, doorOpened, 0); // Rotaci�n cuando la puerta est� 
    }

    // Funci�n para abrir o cerrar la puerta con transici�n
    public void iOpenTheDoor()
    {
        Debug.Log("opening");
        // Si la puerta no est� anim�ndose, entonces comenzamos una animaci�n
        if (!isAnimating)
        {
            isAnimating = true;  // Iniciar animaci�n
            elapsedTime = 0f;    // Reiniciar el tiempo transcurrido
        }
    }

    private void Update()
    {
        // Solo realizar la animaci�n si estamos animando
        if (isAnimating)
        {
            // Acumulamos el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Calculamos el porcentaje completado de la animaci�n
            float completedPercent = elapsedTime / rotationTime;

            // Si la puerta est� cerrada, la abrimos
            if (!doorState)
            {
                transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, completedPercent);
            }
            // Si la puerta est� abierta, la cerramos
            else
            {
                transform.rotation = Quaternion.Lerp(finalRotation, initialRotation, completedPercent);
            }

            // Si la animaci�n ha terminado
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
