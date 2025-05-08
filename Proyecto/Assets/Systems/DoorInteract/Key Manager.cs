using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class KeyManager : NetworkBehaviour
{
    [SerializeField] GameObject key;

    [SerializeField] doorSystem DoorSystem;

    public void nowCanOpenDoor()
    {
        DoorSystem.canInteract = true;
    }
}
