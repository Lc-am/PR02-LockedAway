using UnityEngine;
using TMPro;  
using UnityEngine.EventSystems;  

public class Button_Color : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;  // Texto del botón
    public string normalColorHex = "#8C8C8C";
    public Color normalColor;  // Color del texto
    public Color hoverColor = Color.white;  // Color cuando el ratón está encima

    void Start()
    {
        // Convertir el valor hexadecimal a Color
        ColorUtility.TryParseHtmlString(normalColorHex, out normalColor);
    }

    // Método cuando el ratón entra en el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor; 
    }

    // Método cuando el ratón sale del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor; 
    }
}

