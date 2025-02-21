using UnityEngine;

public class plate : MonoBehaviour
{
    [SerializeField] puzzleConditions puzzleConditions;

    //Llamada al evento de puzzleConditions
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            puzzleConditions.openTheDoor();
        }
    }
}