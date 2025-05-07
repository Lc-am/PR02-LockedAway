using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class curvedPipeController : MonoBehaviour, IInteractable
{
    [SerializeField] public SplineContainer splineContainerCurved;
    [SerializeField] Vector3 pipeRotation1;
    [SerializeField] Vector3 pipeRotation2;
    private bool rotated = false;

    private Quaternion rotationToAdd;
    public bool canRotate = true;

    private void pipeRotate()
    {
        if (rotated)
        {
            transform.rotation = Quaternion.Euler(pipeRotation2);
            rotated = false;
            Debug.Log("rotated");
        }
        else
        {
            transform.rotation = Quaternion.Euler(pipeRotation1);
            rotated = true;
            Debug.Log(" not rotated");
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
