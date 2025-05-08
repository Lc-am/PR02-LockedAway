using Unity.Netcode;
using UnityEngine;

public class KeyDoor : NetworkBehaviour, IInteractable
{
    [SerializeField] KeyManager keyManager;

    public void StartInteraction()
    {
        Destroy(gameObject);
        keyManager.nowCanOpenDoor();
        
    }
}
