using UnityEngine;
using TMPro;  // Aseg�rate de tener la referencia a TextMesh Pro
using UnityEngine.EventSystems;  // Necesario para los eventos de interacci�n con el rat�n

public class Button_Color : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;  // Referencia al texto del bot�n TextMesh Pro
    public string normalColorHex = "#8C8C8C";
    public Color normalColor;  // Color normal del texto
    public Color hoverColor = Color.white;  // Color cuando el rat�n est� encima

    void Start()
    {
        // Convertir el valor hexadecimal a Color
        ColorUtility.TryParseHtmlString(normalColorHex, out normalColor);
    }

    // M�todo cuando el rat�n entra en el bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;  // Cambia el color a hover
    }

    // M�todo cuando el rat�n sale del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor;  // Vuelve al color normal
    }
}

