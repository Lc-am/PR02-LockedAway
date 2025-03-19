using UnityEngine;

public class SphereCreator : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject spherePipePuzzlePrefab;
    private void CreateSphere()
    {
        Instantiate(spherePipePuzzlePrefab, transform.position, Quaternion.identity);
    }

    void IInteractable.StartInteraction()
    {
        CreateSphere();
    }
}
