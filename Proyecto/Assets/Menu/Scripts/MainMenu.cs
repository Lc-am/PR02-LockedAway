using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference skipAction;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject continueText;
    private bool buttonsShown = false;

    [SerializeField] private Canvas Canvas;
    
    [SerializeField]  private Button playButton;
    [SerializeField]  private Button optionsButton;
    [SerializeField]  private Button exitButton;

    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private CanvasGroup optionsMenuCanvasGroup;
    [SerializeField] private CanvasGroup titleMenuCanvasGroup;

    private void Update()
    {
        if (skipAction.action.triggered)
        {
            if (!buttonsShown && skipAction.action.triggered)
            {
                buttonsPanel.SetActive(true); // Muestra los botones
                if (continueText != null) continueText.SetActive(false); // Oculta el texto
                buttonsShown = true;
            }

            MainMenuCanvas();
        }
    }

    void OnEnable()
    {
        skipAction.action.Enable();

        playButton.onClick.AddListener(playLevel);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(exitButtonClick);
    }

    void OnDisable()
    {
        skipAction.action.Disable();

        playButton.onClick.RemoveListener(playLevel);
        optionsButton.onClick.RemoveListener(OpenOptionsMenu);
        exitButton.onClick.RemoveListener(exitButtonClick);
    }

    void MainMenuCanvas()
    {
        
        ShowCanvasGroup(mainMenuCanvasGroup, true);
    }

    void playLevel() 
    {
        SceneManager.LoadScene("Testing");
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
        ShowCanvasGroup(titleMenuCanvasGroup, false);
        ShowCanvasGroup(mainMenuCanvasGroup, false);
    }

    public void CloseOptionsMenu()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(titleMenuCanvasGroup, true);
        ShowCanvasGroup(mainMenuCanvasGroup, true);
    }

    void ShowCanvasGroup(CanvasGroup canvasGroup, bool show)
    {
        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }
}
