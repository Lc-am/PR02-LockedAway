using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class pipeController : NetworkBehaviour, IInteractable
{
    [Header("If Curved")]   //Aplica a las tuberias que estan curvadas
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar
    [SerializeField] private GameObject[] controlPointCurved;    //puntos por los que pasara la esfera en la tuberia curva (deberian haber 3)

    [Header("If straight")] //Aplicar solo si es una tuberia recta
    [SerializeField] private bool isStraight = false;
    [SerializeField] private GameObject[] controlPointStraight;  //puntos por los que pasara la esfera en la tuberia recta (deberian haber 2)

    [Header("Generics")]
    private bool needChangePoints = true;   //Este solo es para reordenar los puntos de control
    private Quaternion rotationToAdd;
    public bool canRotate = true;
    public Transform[] controlPointPosition;    //La posicion de los puntos de control que coge la esfera

    private void Awake()
    {
        rotationToAdd = Quaternion.Euler(neededRotation);
        
        if (isStraight)
        {
            controlPointPosition = new Transform[1];
            controlPointPosition[0].position = controlPointStraight[0].transform.position;
            controlPointPosition[1].position = controlPointStraight[1].transform.position;

            controlPointCurved[0] = null;
            controlPointCurved[1] = null;
            controlPointCurved[2] = null;
        }
        else
        {
            controlPointPosition = new Transform[2];
            controlPointPosition[0].position = controlPointCurved[0].transform.position;
            controlPointPosition[1].position = controlPointCurved[1].transform.position;
            controlPointPosition[2].position = controlPointCurved[2].transform.position;

            controlPointStraight[0] = null;
            controlPointStraight[1] = null;
        }
    }

    public void changeControlPointPositions()
    {
        if(needChangePoints)
        {
            if (isStraight)
            {
                controlPointPosition = new Transform[1];
                controlPointPosition[0].position = controlPointStraight[1].transform.position;
                controlPointPosition[1].position = controlPointStraight[0].transform.position;
            }
            else
            {
                controlPointPosition = new Transform[2];
                controlPointPosition[0].position = controlPointCurved[2].transform.position;
                controlPointPosition[1].position = controlPointCurved[1].transform.position;
                controlPointPosition[2].position = controlPointCurved[0].transform.position;
            }
        }

        needChangePoints = false;
    }


    public void triggerFirstPoint()
    {
        needChangePoints = false;
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
