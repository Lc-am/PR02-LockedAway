using UnityEngine;
using TMPro; 
using DG.Tweening;

public class PulseTextFade : MonoBehaviour
{
    [Header("Referencia al componente TextMeshProUGUI")]
    public TextMeshProUGUI texto;

    [Header("Duración de cada ciclo de fade (segundos)")]
    public float pulseDuration = 1f;

    void Start()
    {
        // Asegurarse de que el texto empieza invisible
        texto.alpha = 0;

        // Animar la opacidad: fade in y fade out de forma continua
        texto.DOFade(1, pulseDuration)
             .SetLoops(-1, LoopType.Yoyo);
    }
}
