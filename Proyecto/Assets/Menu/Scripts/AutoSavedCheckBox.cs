using UnityEngine;
using UnityEngine.UI;

public class AutoSavedCheckBox : MonoBehaviour
{
    [SerializeField] private string prefKey; 
    [SerializeField] private bool defaultValue = false;        

    private Toggle toggle;

    protected virtual void Awake()
    {
        toggle = GetComponent<Toggle>();
        bool savedValue = PlayerPrefs.GetInt(prefKey, defaultValue ? 1 : 0) == 1;
        toggle.isOn = savedValue;
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    protected virtual void Start()
    {
        InternalValueChanged(toggle.isOn);
    }

    private void OnToggleValueChanged(bool newValue)
    {
        PlayerPrefs.SetInt(prefKey, newValue ? 1 : 0);
        PlayerPrefs.Save();
        InternalValueChanged(newValue);
    }

    protected virtual void InternalValueChanged(bool value)
    {
        // Las clases derivadas deben sobreescribirla para personalizar el comportamiento
    }
}
