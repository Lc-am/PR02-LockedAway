using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject activableObject;

    private void OnMouseDown()
    {
        if (activableObject == null)
        {
            Debug.LogError(" activableObject no está asignado en el Inspector.", this);
            return;
        }

        IActivable activable = activableObject.GetComponent<IActivable>();
        if (activable != null)
        {
            activable.Activate();
        }
        else
        {
            Debug.LogError(" El objeto asignado no implementa IActivable.", this);
        }
    }
}
