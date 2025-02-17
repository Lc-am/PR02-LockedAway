using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Resolution : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown; 

    private void Awake()
    {
        PopulateDropdown();
        LoadSavedResolution();

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void PopulateDropdown()
    {
        resolutionDropdown.ClearOptions();

        UnityEngine.Resolution[] resolutions = Screen.resolutions; ;
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex; 
        resolutionDropdown.RefreshShownValue(); 
    }

    private void LoadSavedResolution()
    {
        // Carga la resoluci�n guardada de PlayerPrefs
        int savedIndex = PlayerPrefs.GetInt("ScreenResolutionIndex", resolutionDropdown.value);
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
        OnResolutionChanged(savedIndex); 
    }

    private void OnResolutionChanged(int index)
    {
        UnityEngine.Resolution[] resolutions = Screen.resolutions; 
        if (index < resolutions.Length)
        {
            UnityEngine.Resolution selectedResolution = resolutions[index];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
            PlayerPrefs.SetInt("ScreenResolutionIndex", index); 
            PlayerPrefs.Save(); 
        }
    }
}
