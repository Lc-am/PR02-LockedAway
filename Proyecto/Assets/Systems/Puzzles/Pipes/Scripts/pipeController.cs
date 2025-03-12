using UnityEngine;

public class pipeController : MonoBehaviour
{
    [SerializeField] private int correctState;
    private int currentState;
    [SerializeField] private Vector3 starterRotation;   //poner la rotacion inicial
    [SerializeField] private Vector3 neededRotation;    //poner la suma que se quiere rotar
    private Quaternion rotationToAdd;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(starterRotation);
        rotationToAdd = Quaternion.Euler(neededRotation);
    }

    private void Update()
    {
        
    }

    public void pipeRotate()
    {
        transform.rotation *= rotationToAdd;

        if(currentState <=3)
        {
            currentState++;
        }
        else
        {
            currentState = 0;
        }
    }
}
