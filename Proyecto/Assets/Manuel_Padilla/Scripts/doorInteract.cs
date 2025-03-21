using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class doorInteract : NetworkBehaviour
{
    public UnityEvent doorInteraction;

    public void Interact()
    {
        doorInteraction.Invoke();
    }
}