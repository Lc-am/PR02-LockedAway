using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class pipeController : NetworkBehaviour, IInteractable
{
    [Header("If Curved")]   //Aplica a las tuberias que estan curvadas
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar
    [SerializeField] public GameObject[] controlPointCurved;    //puntos por los que pasara la esfera en la tuberia curva (deberian haber 3)

    [Header("If straight")] //Aplicar solo si es una tuberia recta
    [SerializeField] public bool isStraight = false;
    [SerializeField] public GameObject[] controlPointStraight;  //puntos por los que pasara la esfera en la tuberia recta (deberian haber 2)

    [Header("Generics")]
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
        if(canRotate)
        {
            pipeRotate();
        }
    }
}
