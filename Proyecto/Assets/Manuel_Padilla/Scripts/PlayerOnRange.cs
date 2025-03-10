using UnityEngine;

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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canvasCrosshair.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("UnlockCursor"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canvasCrosshair.SetActive(true);
        }
    }
}
