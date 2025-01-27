using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]  private Button playButton;
    [SerializeField]  private Button optionsButton;
    [SerializeField]  private Button exitButton;

    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private CanvasGroup optionsMenuCanvasGroup;

    void OnEnable()
    {
        playButton.onClick.AddListener(playLevel);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(exitButtonClick);
    }

    void OnDisable()
    {
        playButton.onClick.RemoveListener(playLevel);
        optionsButton.onClick.RemoveListener(OpenOptionsMenu);
        exitButton.onClick.RemoveListener(exitButtonClick);
    }

    void playLevel() 
    {
        SceneManager.LoadScene("Jugable1");
    }

    void exitButtonClick()
    {

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    void OpenOptionsMenu()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, true);
        ShowCanvasGroup(mainMenuCanvasGroup, false);
    }

    public void CloseOptionsMenu()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(mainMenuCanvasGroup, true);
    }

    void ShowCanvasGroup(CanvasGroup canvasGroup, bool show)
    {
        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }
}
