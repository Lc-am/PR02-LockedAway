using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class pipeController : NetworkBehaviour, IInteractable
{
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar
    [SerializeField] public GameObject[] controlPoint;
    public Transform[] controlPointPositions;

    private Quaternion rotationToAdd;
    public bool canRotate = true;

    private void Awake()
    {
        rotationToAdd = Quaternion.Euler(neededRotation);
    }

    //Cambia las posiciones de los puntos de control para que punto de control 1 esta en la posicion del 3 y viceversa
    public void changeControlPositions()
    {
        controlPointPositions[0].position = controlPoint[2].transform.position;
        controlPointPositions[1].position = controlPoint[1].transform.position;
        controlPointPositions[2].position = controlPoint[0].transform.position;
    }

    private void pipeRotate()
    {
        transform.rotation *= rotationToAdd;    //Añade la rotacion puesta a la rotacion actual
    }

    void IInteractable.StartInteraction()
    {
        if(canRotate)
        {
            pipeRotate();
        }
    }
}
