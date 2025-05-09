using Unity.Netcode;
using UnityEngine;
using UnityEngine.Splines;

public class SphereCreator : NetworkBehaviour, IInteractable
{
    [SerializeField] private GameObject spherePipePuzzlePrefab;
    [SerializeField] public SplineContainer firstPipe;

    followSpline followspline;

    [ClientRpc]
    private void CreateSphereClientRpc()
    {
        Instantiate(spherePipePuzzlePrefab, transform.position, Quaternion.identity);
        followspline = spherePipePuzzlePrefab.GetComponent<followSpline>();
        followspline.ruta = firstPipe;
    }

    private void CreateSphere()
    {
        Debug.Log("pulsado");
        Instantiate(spherePipePuzzlePrefab, transform.position, Quaternion.identity);
        followspline = spherePipePuzzlePrefab.GetComponent<followSpline>();
        followspline.ruta = firstPipe;
    }

    void IInteractable.StartInteraction()
    {
        CreateSphere();
    }
}
