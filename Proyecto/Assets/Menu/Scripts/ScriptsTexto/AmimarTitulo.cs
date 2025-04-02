using UnityEngine;
using TMPro; 
using DG.Tweening;

public class FadeInText : MonoBehaviour
{
    [Header("Referencia al componente TextMeshProUGUI")]
    public TextMeshProUGUI texto;

    [Header("Duración del fade in en segundos")]
    public float duracionFade = 2f;

    void Start()
    {
        // Asegurarse de que el texto empieza invisible
        texto.alpha = 0;

        // Realizar el fade in, aumentando la opacidad de 0 a 1 en "duracionFade" segundos
        texto.DOFade(1, duracionFade)
        .SetLoops(-1, LoopType.Yoyo);
    }
}
