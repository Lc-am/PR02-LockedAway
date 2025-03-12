using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerOnRange : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private GameObject canvasCrosshair;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("UnlockCursor"))
        {
            //Si entra en el area de un objeto activa el cursor.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canvasCrosshair.SetActive(false);
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
