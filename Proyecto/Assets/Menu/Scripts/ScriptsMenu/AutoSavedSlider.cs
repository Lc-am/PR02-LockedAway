using UnityEngine;
using UnityEngine.UI;

public class AutoSavedSlider : MonoBehaviour
{
    [SerializeField] private string prefKey;
    [SerializeField] private float defaultValue = 0.5f;

    float savedValue;
    private Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
        if (PlayerPrefs.GetFloat(prefKey, defaultValue) != null)
        {
            savedValue = PlayerPrefs.GetFloat(prefKey, defaultValue);
        }
        else
        {
            savedValue = defaultValue;
        }

        slider.value = savedValue;
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    protected virtual void Start()
    {
        InternalValueChanged(slider.value);
    }

    private void OnSliderValueChanged(float newValue)
    {
        PlayerPrefs.SetFloat(prefKey, newValue);
        PlayerPrefs.Save();
        InternalValueChanged(newValue);
    }

    protected virtual void InternalValueChanged(float value)
    {
        // Método virtual que será sobreescrito en las clases derivadas
    }
}
