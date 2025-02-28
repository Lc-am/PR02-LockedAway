using Unity.Netcode;
using UnityEngine;

public class plate : NetworkBehaviour
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