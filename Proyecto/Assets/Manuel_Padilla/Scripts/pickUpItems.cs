using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class pickUpItems : NetworkBehaviour
{
    [SerializeField] InputActionReference pickUp;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject holdPos;    // GameObject en el que estar� el objeto cogido

    [SerializeField] private float pickUpRange = 5f;

    [SerializeField] private Transform camera;

    private GameObject heldObject;  // Variable para saber qu� gameObject hemos cogido
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
        if (heldObject != null && IsOwner) // Solo mover el objeto si este es propiedad de este jugador
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
        if (heldObject == null)
        {
            RaycastHit hit;

            // Cada vez que se da el click, lanza un rayo para ver si golpea con un tag de pickable para coger el objeto
            if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
                if (hit.transform.CompareTag("Pickeable"))
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
        }
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        if (heldObject != null)
        {
            StopClipping();

            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), false);
            heldObjectRB.isKinematic = false;

            // Aqu� el jugador suelta el objeto, cambia la propiedad de vuelta al host o al servidor.
            if (IsOwner)
            {
                // Si el jugador es el due�o, puede soltar el objeto y cambiar la propiedad.
                NetworkObject objectNetworkObject = heldObject.GetComponent<NetworkObject>();
                objectNetworkObject.ChangeOwnership(NetworkManager.LocalClientId);  // Volver al host
            }

            heldObject = null;
        }
    }

    private void PickUpObject(GameObject gameObject)
    {
        // Transforma el objeto cogible para que se mueva en el holdPos
        if (gameObject.transform.GetComponent<Rigidbody>())
        {
            heldObject = gameObject;
            heldObjectRB = gameObject.transform.GetComponent<Rigidbody>();
            heldObjectRB.isKinematic = true;
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), true);

            // Si no somos el propietario del objeto, cambian la propiedad al cliente que lo agarra
            if (!IsOwner)
            {
                NetworkObject objectNetworkObject = heldObject.GetComponent<NetworkObject>();
                objectNetworkObject.ChangeOwnership(NetworkManager.LocalClientId);
            }
        }
    }

    private void StopClipping()
    {
        // Funci�n para que cuando el objeto se quede en medio de otro objeto vuelva a una superficie vac�a
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













//using System;
//using Unity.Netcode;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class pickUpItems : NetworkBehaviour
//{
//    [SerializeField] InputActionReference pickUp;

//    [SerializeField] private GameObject player;
//    [SerializeField] private GameObject holdPos;    //Gameobject en el que estara el objeto cogido

//    [SerializeField] private float pickUpRange = 5f;

//    [SerializeField] private Transform camera;

//    private GameObject heldObject;  //Variable para saber que gameobject hemos cogido
//    private Rigidbody heldObjectRB; //Su rigidbody

//    private void OnEnable()
//    {
//        pickUp.action.started += OnPickUp;
//        pickUp.action.performed += OnPickUp;
//        pickUp.action.canceled += OnDrop;

//        pickUp.action.Enable();
//    }

//    private void OnDisable()
//    {
//        pickUp.action.started -= OnPickUp;
//        pickUp.action.performed -= OnPickUp;
//        pickUp.action.canceled -= OnDrop;

//        pickUp.action.Disable();
//    }

//    private void Update()
//    {
//        if(heldObject != null)
//        {
//            MoveObject();
//        }
//    }

//    private void MoveObject()
//    {
//        heldObject.transform.position = holdPos.transform.position;
//    }

//    private void OnPickUp(InputAction.CallbackContext context)
//    {
//        if (heldObject == null)
//        {
//            RaycastHit hit;

//            //Cada vez que se da el click lanza un razo para ver si golpea con un tag de pcikeable para coger el objeto
//            if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, pickUpRange))
//            {
//                if (hit.transform.CompareTag("Pickeable"))
//                {
//                    PickUpObject(hit.transform.gameObject);
//                }

//            }
//        }
//    }

//    private void OnDrop(InputAction.CallbackContext context)
//    {
//        if (heldObject != null)
//        {
//            StopClipping();

//            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), false);
//            heldObjectRB.isKinematic = false;
//            heldObject = null;
//        }
//    }

//    private void PickUpObject(GameObject gameObject)
//    {
//        //transforma el objeto cogible para que se mueva en el holdPos
//        if(gameObject.transform.GetComponent<Rigidbody>())
//        {
//            heldObject = gameObject;
//            heldObjectRB = gameObject.transform.GetComponent<Rigidbody>();
//            heldObjectRB.isKinematic = true;
//            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), true);

//            if (IsOwner) return;
//            NetworkObject objectNetworkObject = GetComponent<NetworkObject>();
//            objectNetworkObject.ChangeOwnership(NetworkManager.LocalClientId);
//        }
//    }

//    private void StopClipping()
//    {
//        //Funcion para que cuando el objeto se quede en medio de otro objeto vuelva a una superficie vacia
//        if (heldObject == null) return;

//        var clipRange  = Vector3.Distance(heldObject.transform.position, transform.position);

//        RaycastHit[] hits;
//        hits = Physics.RaycastAll(camera.position, camera.TransformDirection(Vector3.forward), clipRange);

//        if(hits.Length > 1)
//        {
//            heldObject.transform.position = camera.position + new Vector3(0f, -0.5f, 0f);
//        }
//    }
//}
