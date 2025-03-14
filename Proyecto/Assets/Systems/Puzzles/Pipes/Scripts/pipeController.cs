using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class pipeController : NetworkBehaviour, IInteractable
{
    [SerializeField] private int correctState;
    private int currentState = 0;

    [SerializeField] private Vector3 starterRotation;   //poner la rotacion inicial
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar

    private Quaternion rotationToAdd;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(starterRotation);
        rotationToAdd = Quaternion.Euler(neededRotation);
    }

    public void pipeRotate()
    {
        Debug.Log("Rotating pipe");
        transform.rotation *= rotationToAdd;    //Añade la rotacion puesta a la rotacion actual

        if (currentState <=3)   //Actualiza el estado para revisar si esta bien o no
        {
            currentState++;
        }
        else
        {
            currentState = 0;
        }

        if(currentState == correctState)
        {
            Debug.Log("Pipe is correct");
        }
    }

    void IInteractable.StartInteraction()
    {
        pipeRotate();
    }
}
