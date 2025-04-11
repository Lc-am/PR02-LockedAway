using UnityEngine;
using UnityEngine.Splines;

public class SphereCreator : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject spherePipePuzzlePrefab;
    [SerializeField] public SplineContainer firstPipe;

    followSpline followspline;
    private void CreateSphere()
    {
        Instantiate(spherePipePuzzlePrefab, transform.position, Quaternion.identity);
        followspline = spherePipePuzzlePrefab.GetComponent<followSpline>();
        followspline.ruta = firstPipe;
    }

    void IInteractable.StartInteraction()
    {
        CreateSphere();
    }
}
