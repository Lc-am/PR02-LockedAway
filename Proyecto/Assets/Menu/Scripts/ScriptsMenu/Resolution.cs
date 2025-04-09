//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;


//public class Resolution : MonoBehaviour
//{
//    [SerializeField] private TMP_Dropdown resolutionDropdown; 

//    private void Awake()
//    {
//        PopulateDropdown();
//        LoadSavedResolution();

//        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
//    }

//    private void PopulateDropdown()
//    {
//        resolutionDropdown.ClearOptions();

//        UnityEngine.Resolution[] resolutions = Screen.resolutions; ;
//        int currentResolutionIndex = 0;
//        List<string> options = new List<string>();

//        for (int i = 0; i < resolutions.Length; i++)
//        {
//            string option = resolutions[i].width + " x " + resolutions[i].height;
//            options.Add(option);

//            if (resolutions[i].width == Screen.currentResolution.width &&
//                resolutions[i].height == Screen.currentResolution.height)
//            {
//                currentResolutionIndex = i;
//            }
//        }

//        resolutionDropdown.AddOptions(options);
//        resolutionDropdown.value = currentResolutionIndex; 
//        resolutionDropdown.RefreshShownValue(); 
//    }

//    private void LoadSavedResolution()
//    {
//        // Carga la resolución guardada de PlayerPrefs
//        int savedIndex = PlayerPrefs.GetInt("ScreenResolutionIndex", resolutionDropdown.value);
//        resolutionDropdown.value = savedIndex;
//        resolutionDropdown.RefreshShownValue();
//        OnResolutionChanged(savedIndex); 
//    }

//    private void OnResolutionChanged(int index)
//    {
//        UnityEngine.Resolution[] resolutions = Screen.resolutions; 
//        if (index < resolutions.Length)
//        {
//            UnityEngine.Resolution selectedResolution = resolutions[index];
//            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
//            PlayerPrefs.SetInt("ScreenResolutionIndex", index); 
//            PlayerPrefs.Save(); 
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private readonly List<Vector2Int> commonResolutions = new List<Vector2Int>
    {
        new Vector2Int(1920, 1080),  
        new Vector2Int(2560, 1440),  
        new Vector2Int(3840, 2160),  
        new Vector2Int(1280, 720),   
        new Vector2Int(1600, 900),   
        new Vector2Int(1366, 768),   
        new Vector2Int(1024, 768)    
    };

    private void Awake()
    {
        PopulateDropdown();
        LoadSavedResolution();

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void PopulateDropdown()
    {
        resolutionDropdown.ClearOptions();

        Dictionary<string, UnityEngine.Resolution> uniqueResolutions = new Dictionary<string, UnityEngine.Resolution>();

        UnityEngine.Resolution[] resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            Vector2Int resolution = new Vector2Int(resolutions[i].width, resolutions[i].height);
            if (commonResolutions.Contains(resolution))
            {
                string resolutionKey = resolutions[i].width + " x " + resolutions[i].height;

                if (!uniqueResolutions.ContainsKey(resolutionKey))
                {
                    uniqueResolutions.Add(resolutionKey, resolutions[i]);
                    options.Add(resolutionKey);
                }

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = options.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void LoadSavedResolution()
    {
        int savedIndex = PlayerPrefs.GetInt("ScreenResolutionIndex", resolutionDropdown.value);
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
        OnResolutionChanged(savedIndex);
    }

    private void OnResolutionChanged(int index)
    {
        UnityEngine.Resolution[] resolutions = Screen.resolutions;
        Dictionary<string, UnityEngine.Resolution> uniqueResolutions = new Dictionary<string, UnityEngine.Resolution>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            Vector2Int resolution = new Vector2Int(resolutions[i].width, resolutions[i].height);
            if (commonResolutions.Contains(resolution))
            {
                string resolutionKey = resolutions[i].width + " x " + resolutions[i].height;
                if (!uniqueResolutions.ContainsKey(resolutionKey))
                {
                    uniqueResolutions.Add(resolutionKey, resolutions[i]);
                }
            }
        }

        string selectedResolutionKey = resolutionDropdown.options[index].text;
        UnityEngine.Resolution selectedResolution = uniqueResolutions[selectedResolutionKey];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ScreenResolutionIndex", index);
        PlayerPrefs.Save();
    }
}



