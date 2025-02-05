using UnityEngine;
using UnityEngine.InputSystem;

public class ShowMenuButtons : MonoBehaviour
{
    [Header("Panel con los botones del men�")]
    [SerializeField] private GameObject buttonsPanel;

    [Header("Texto de 'Pulsa espacio para continuar'")]
    [SerializeField] private GameObject continueText;

    [Header("Acci�n de input para continuar")]
    [SerializeField] private InputActionReference continueAction;

    private bool buttonsShown = false;

    private void OnEnable()
    {
        continueAction.action.Enable();
    }

    private void OnDisable()
    {
        continueAction.action.Disable();
    }

    void Update()
    {
        // Si se presiona la acci�n (ej. barra espaciadora) y a�n no se han mostrado los botones:
        if (!buttonsShown && continueAction.action.triggered)
        {
            buttonsPanel.SetActive(true); // Muestra los botones
            if (continueText != null) continueText.SetActive(false); // Oculta el texto
            buttonsShown = true;
        }
    }
}
