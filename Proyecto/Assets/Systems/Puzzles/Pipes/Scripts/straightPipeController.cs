using UnityEngine;
using UnityEngine.Splines;

public class straightPipeController : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar
    [SerializeField] public SplineContainer splineContainerStraight;

    private Quaternion rotationToAdd;
    public bool canRotate = true;
    private void Awake()
    {
        rotationToAdd = Quaternion.Euler(neededRotation);
    }

    private void pipeRotate()
    {
        transform.rotation *= rotationToAdd;    //Añade la rotacion puesta a la rotacion actual
    }

    void IInteractable.StartInteraction()
    {
        if (canRotate)
        {
            pipeRotate();
        }
    }
}
