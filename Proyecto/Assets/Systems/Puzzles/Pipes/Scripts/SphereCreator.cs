using UnityEngine;
using UnityEngine.Splines;

public class SphereCreator : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject spherePipePuzzlePrefab;
    [SerializeField] public SplineContainer firstPipe;
    private void CreateSphere()
    {
        Instantiate(spherePipePuzzlePrefab, transform.position, Quaternion.identity);
    }

    void IInteractable.StartInteraction()
    {
        CreateSphere();
    }
}
