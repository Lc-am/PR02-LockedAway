using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class interactItems : NetworkBehaviour
{
    [Header("Interactions")]
    [SerializeField] private InputActionReference interact;
    [SerializeField] private float interactRange = 5f;
    [SerializeField] private Transform camera;

    private void OnEnable()
    {
        interact.action.Enable();

        interact.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interact.action.Disable();

        interact.action.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interacting");
        //Funcion que lanza un raycast cada vez que se pulsa el boton de interacción para saber si puede y que puede hacer el objeto interactuado.
        if (IsLocalPlayer)
        {
            RaycastHit hit;
            Debug.Log("Interacting");

            if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, interactRange))
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if (interactable != null)
                    { interactable.StartInteraction(); }
            }
        }
    }
}