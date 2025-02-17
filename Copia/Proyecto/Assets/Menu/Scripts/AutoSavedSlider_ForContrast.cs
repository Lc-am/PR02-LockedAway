using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class AutoSavedSlider_ForContrast : AutoSavedSlider
{
    [SerializeField] private Volume globalVolume;    
    [SerializeField] private float minContrast = -50f; 
    [SerializeField] private float maxContrast = 100f; 
    private ColorAdjustments colorAdjustments;

    protected override void Awake()
    {
        base.Awake();

        if (globalVolume != null && globalVolume.profile.TryGet(out colorAdjustments))
        {
            // ColorAdjustments fue encontrado en el perfil del Volume
        }
    }

    protected override void InternalValueChanged(float value)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.contrast.value = Mathf.Lerp(minContrast, maxContrast, value);
        }
    }
}
