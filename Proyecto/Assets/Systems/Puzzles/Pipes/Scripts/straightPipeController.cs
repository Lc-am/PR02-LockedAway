using UnityEngine;

public class straightPipeController : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar
    [SerializeField] private GameObject[] controlPointStraight;  //puntos por los que pasara la esfera en la tuberia recta (deberian haber 2)

    private bool needChangePoints = true;   //Este solo es para reordenar los puntos de control
    private Quaternion rotationToAdd;
    public bool canRotate = true;
    public Transform[] controlPointPosition;    //La posicion de los puntos de control que coge la esfera

    private void Awake()
    {
        rotationToAdd = Quaternion.Euler(neededRotation);

        controlPointPosition[0].position = controlPointStraight[0].transform.position;
        controlPointPosition[1].position = controlPointStraight[1].transform.position;
    }

    public void changeControlPointPositionsStraight()
    {
        if (needChangePoints)
        {
            controlPointPosition[0].position = controlPointStraight[1].transform.position;
            controlPointPosition[1].position = controlPointStraight[0].transform.position;
        }

        needChangePoints = false;
    }

    public void triggerFirstPointStraight()
    {
        needChangePoints = false;
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
