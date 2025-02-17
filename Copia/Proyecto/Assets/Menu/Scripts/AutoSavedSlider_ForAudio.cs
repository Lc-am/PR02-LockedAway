using UnityEngine;
using UnityEngine.Audio;

public class AutoSavedSlider_ForAudio : AutoSavedSlider
{
    [SerializeField] private AudioMixer audioMixer; 
    [SerializeField] private string parameterName;  
    protected override void InternalValueChanged(float value)
    {
        float dBValue = LinearToDecibel(value);
        audioMixer.SetFloat(parameterName, dBValue);
    }

    private float LinearToDecibel(float linear)
    {
        float dB;
        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f; 
        return dB;
    }
}

