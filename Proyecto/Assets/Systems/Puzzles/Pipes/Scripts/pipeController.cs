using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class pipeController : NetworkBehaviour, IInteractable
{
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar

    private Quaternion rotationToAdd;

    private void Awake()
    {
        //transform.rotation = Quaternion.Euler(starterRotation);
        rotationToAdd = Quaternion.Euler(neededRotation);
    }

    public void pipeRotate()
    {
        transform.rotation *= rotationToAdd;    //Añade la rotacion puesta a la rotacion actual
    }

    void IInteractable.StartInteraction()
    {
        pipeRotate();
    }
}
