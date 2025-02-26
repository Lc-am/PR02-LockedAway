using UnityEngine;
using UnityEngine.Events;

public class doorInteract : MonoBehaviour
{
    public UnityEvent doorInteraction;

    public void Interact()
    {
        doorInteraction.Invoke();
    }
}