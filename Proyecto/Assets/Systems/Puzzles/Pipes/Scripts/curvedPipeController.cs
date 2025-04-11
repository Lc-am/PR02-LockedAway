using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class curvedPipeController : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar
    [SerializeField] public SplineContainer splineContainerCurved;
    [SerializeField] Vector3 pipeRotation1;
    [SerializeField] Vector3 pipeRotation2;
    private bool rotated = false;

    private Quaternion rotationToAdd;
    public bool canRotate = true;

    private void Awake()
    {
        rotationToAdd = Quaternion.Euler(neededRotation);
    }

    private void pipeRotate()
    {
        if (rotated)
        {
            transform.rotation = Quaternion.Euler(pipeRotation2);
            rotated = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(pipeRotation1);
            rotated = true;
        }
    }

    void IInteractable.StartInteraction()
    {
        if (canRotate)
        {
            pipeRotate();
        }
    }
}
