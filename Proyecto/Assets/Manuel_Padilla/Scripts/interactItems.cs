using UnityEngine;
using UnityEngine.InputSystem;

public class interactItems : MonoBehaviour
{
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
        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, interactRange))
        {
            if (hit.transform.CompareTag("Door"))
            {
                doorInteract door = hit.transform.GetComponent<doorInteract>();
                if (door != null)
                {
                    door.Interact();
                }
            }
        }
    }
}