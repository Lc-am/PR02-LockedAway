using UnityEngine;
using TMPro;  
using UnityEngine.EventSystems;  

public class Button_Color : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;  // Texto del bot�n
    public string normalColorHex = "#8C8C8C";
    public Color normalColor;  // Color del texto
    public Color hoverColor = Color.white;  // Color cuando el rat�n est� encima

    void Start()
    {
        // Convertir el valor hexadecimal a Color
        ColorUtility.TryParseHtmlString(normalColorHex, out normalColor);
    }

    // M�todo cuando el rat�n entra en el bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor; 
    }

    // M�todo cuando el rat�n sale del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor; 
    }
}

