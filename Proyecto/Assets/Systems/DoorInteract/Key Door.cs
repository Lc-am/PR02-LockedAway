using Unity.Netcode;
using UnityEngine;

public class KeyDoor : NetworkBehaviour, IInteractable
{
    [SerializeField] KeyManager keyManager;

    private void KeyInteracted()
    {
        Destroy(gameObject);
        keyManager.nowCanOpenDoor();
    }

    [ClientRpc]
    private void KeyInteractedClientRpc()
    {
        KeyInteracted();
    }

    public void StartInteraction()
    {
        KeyInteracted();
    }
}
