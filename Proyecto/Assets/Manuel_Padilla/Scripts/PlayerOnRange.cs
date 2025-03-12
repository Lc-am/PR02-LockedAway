using UnityEngine;

public class PlayerOnRange : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private GameObject canvasCrosshair;
    [SerializeField] private GameObject camera;
    private Vector3 midScreen = Vector3.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        midScreen = new Vector3(Screen.height / 2, Screen.width / 2, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("UnlockCursor"))
        {
            //Si entra en el area de un objeto activa el cursor.
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            //canvasCrosshair.SetActive(false);

            RaycastHit hit;

            // Lanza un raycast constante para activar los eventos de un canvas
            if (Physics.Raycast(camera.transform.position, midScreen, out hit))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("UnlockCursor"))
        {
            //Si sale del area de un objeto desactiva el cursor.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canvasCrosshair.SetActive(true);
        }
    }
}
