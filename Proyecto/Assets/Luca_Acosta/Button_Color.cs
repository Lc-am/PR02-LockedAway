using UnityEngine;
using TMPro;  // Asegúrate de tener la referencia a TextMesh Pro
using UnityEngine.EventSystems;  // Necesario para los eventos de interacción con el ratón

public class Button_Color : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;  // Referencia al texto del botón TextMesh Pro
    public string normalColorHex = "#8C8C8C";
    public Color normalColor;  // Color normal del texto
    public Color hoverColor = Color.white;  // Color cuando el ratón está encima

    void Start()
    {
        // Convertir el valor hexadecimal a Color
        ColorUtility.TryParseHtmlString(normalColorHex, out normalColor);
    }

    // Método cuando el ratón entra en el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;  // Cambia el color a hover
    }

    // Método cuando el ratón sale del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor;  // Vuelve al color normal
    }
}

