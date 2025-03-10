using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class pickUpItems : NetworkBehaviour
{
    [SerializeField] InputActionReference pickUp;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject holdPos;    // GameObject en el que estará el objeto cogido

    [SerializeField] private float pickUpRange = 5f;

    [SerializeField] private Transform camera;

    private GameObject heldObject;  // Variable para saber qué gameObject hemos cogido
    private Rigidbody heldObjectRB; // Su Rigidbody

    private void OnEnable()
    {
        pickUp.action.started += OnPickUp;
        pickUp.action.performed += OnPickUp;
        pickUp.action.canceled += OnDrop;

        pickUp.action.Enable();
    }

    private void OnDisable()
    {
        pickUp.action.started -= OnPickUp;
        pickUp.action.performed -= OnPickUp;
        pickUp.action.canceled -= OnDrop;

        pickUp.action.Disable();
    }

    private void Update()
    {
        if (heldObject) // Solo mover el objeto si este es propiedad de este jugador
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        heldObject.transform.position = holdPos.transform.position;
    }

    private void OnPickUp(InputAction.CallbackContext context)
    {
        if (IsLocalPlayer)
        {
            PickupServerRPC(camera.position, camera.TransformDirection(Vector3.forward));
        }
    }

    [Rpc(SendTo.Server)]
    void PickupServerRPC(Vector3 cameraPosition, Vector3 cameraDirection)
    {
        if (heldObject == null)
        {
            RaycastHit hit;

            // Cada vez que se da el click, lanza un rayo para ver si golpea con un tag de pickable para coger el objeto
            if (Physics.Raycast(cameraPosition, cameraDirection, out hit, pickUpRange))
            {
                if (hit.transform.CompareTag("Pickeable"))
                {
                    PickUpObject(hit.transform.gameObject, holdPos);
                }
            }
        }
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        if (IsLocalPlayer)
        {
            DropObjectServerRPC();
        }
    }

    [Rpc(SendTo.Server)]
    private void DropObjectServerRPC()
    {
        if (heldObject != null)
        {
            StopClipping();

            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), false);
            heldObjectRB.isKinematic = false;

            heldObject = null;
        }
    }

    private void PickUpObject(GameObject pickeableGameObject, GameObject holdGameObject)
    {
        // Esto solo ocurre en el Server - Copón
        if (pickeableGameObject.transform.GetComponent<Rigidbody>())
        {
            heldObject = pickeableGameObject;
            heldObjectRB = heldObject.transform.GetComponent<Rigidbody>();
            heldObjectRB.isKinematic = true;
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), true);
        }
    }

    private void StopClipping()
    {
        // Función para que cuando el objeto se quede en medio de otro objeto vuelva a una superficie vacía
        if (heldObject == null) return;

        var clipRange = Vector3.Distance(heldObject.transform.position, transform.position);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(camera.position, camera.TransformDirection(Vector3.forward), clipRange);

        if (hits.Length > 1)
        {
            heldObject.transform.position = camera.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}