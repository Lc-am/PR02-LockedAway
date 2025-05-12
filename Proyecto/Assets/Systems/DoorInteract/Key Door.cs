using Unity.Netcode;
using UnityEngine;

public class KeyDoor : NetworkBehaviour, IInteractable
{
    [SerializeField] KeyManager keyManager;

    [ClientRpc]
    private void KeyInteractedClientRPC()
    {
        Destroy(gameObject);
        keyManager.nowCanOpenDoor();
    }

    public void StartInteraction()
    {
        KeyInteractedClientRPC();
    }
}
