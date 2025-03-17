using UnityEngine;
using System.Collections; 


public class ButtonTrigger : MonoBehaviour
{
    public DoorController doorController; // Referencia al script de la puerta
    private bool isPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo toc� el bot�n: " + other.name); // Ver en la consola

        if (!isPressed && other.CompareTag("Player"))
        {
            isPressed = true;
            Debug.Log("�El jugador toc� el bot�n!"); // Ver en la consola
            doorController.ToggleDoor();
            StartCoroutine(ResetButton());
        }
    }


    IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de permitir otra activaci�n
        isPressed = false;
    }
}
