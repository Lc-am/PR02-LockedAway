using UnityEngine;

public class plate : MonoBehaviour
{
    [SerializeField] puzzleConditions puzzleConditions;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            puzzleConditions.openTheDoor.Invoke();
        }
    }
}
