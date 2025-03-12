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
//        // Carga la resoluci�n guardada de PlayerPrefs
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

    private void Awake()
    {
        PopulateDropdown();
        LoadSavedResolution();

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void PopulateDropdown()
    {
        resolutionDropdown.ClearOptions();

        // Usamos un diccionario para evitar duplicados de resoluci�n (sin tener en cuenta Hz)
        Dictionary<string, UnityEngine.Resolution> uniqueResolutions = new Dictionary<string, UnityEngine.Resolution>();

        UnityEngine.Resolution[] resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            // Usamos solo el ancho y alto de la resoluci�n para la clave
            string resolutionKey = resolutions[i].width + " x " + resolutions[i].height;

            // Si no est� en el diccionario, lo a�adimos
            if (!uniqueResolutions.ContainsKey(resolutionKey))
            {
                uniqueResolutions.Add(resolutionKey, resolutions[i]);
                options.Add(resolutionKey);
            }

            // Comprobamos si es la resoluci�n actual para seleccionarla por defecto
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = options.Count - 1;
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
        Dictionary<string, UnityEngine.Resolution> uniqueResolutions = new Dictionary<string, UnityEngine.Resolution>();

        // Llenamos el diccionario de resoluciones �nicas
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionKey = resolutions[i].width + " x " + resolutions[i].height;
            if (!uniqueResolutions.ContainsKey(resolutionKey))
            {
                uniqueResolutions.Add(resolutionKey, resolutions[i]);
            }
        }

        // Obtenemos la resoluci�n seleccionada sin duplicados
        string selectedResolutionKey = resolutionDropdown.options[index].text;
        UnityEngine.Resolution selectedResolution = uniqueResolutions[selectedResolutionKey];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        // Guardamos la resoluci�n seleccionada
        PlayerPrefs.SetInt("ScreenResolutionIndex", index);
        PlayerPrefs.Save();
    }
}

