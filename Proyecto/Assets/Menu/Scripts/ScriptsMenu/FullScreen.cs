using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    [SerializeField] private Toggle fullScreenToggle;

    private void Awake()
    {
        LoadSavedFullScreen();
        fullScreenToggle.onValueChanged.AddListener(OnFullScreenToggleChanged);
    }

    private void LoadSavedFullScreen()
    {
        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1; 
        fullScreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    private void OnFullScreenToggleChanged(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0); 
        PlayerPrefs.Save(); 
    }
}
