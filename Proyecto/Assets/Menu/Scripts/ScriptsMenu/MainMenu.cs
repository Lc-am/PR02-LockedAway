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

    [SerializeField] private Button optionsAudio;
    [SerializeField] private Button optionsGame;
    [SerializeField] private Button optionsGraphics;

    [SerializeField] private CanvasGroup optionsMenuAudio;
    [SerializeField] private CanvasGroup optionsMenuGame;
    [SerializeField] private CanvasGroup optionsMenuGraphics;

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


        optionsAudio.onClick.AddListener(OpenAudio);
        optionsGame.onClick.AddListener(OpenGame);
        optionsGraphics.onClick.AddListener(OpenGraphics);
    }

    void OnDisable()
    {
        skipAction.action.Disable();

        playButton.onClick.RemoveListener(playLevel);
        optionsButton.onClick.RemoveListener(OpenOptionsMenu);
        exitButton.onClick.RemoveListener(exitButtonClick);

        optionsAudio.onClick.RemoveListener(OpenAudio);
        optionsGame.onClick.RemoveListener(OpenGame);
        optionsGraphics.onClick.RemoveListener(OpenGraphics);
    }

    void MainMenuCanvas()
    {
        ShowCanvasGroup(mainMenuCanvasGroup, true);
    }

    void playLevel() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene"); // REVISAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAR
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

    public void CloseOptions()
    {
        if(optionsMenuCanvasGroup.alpha == 1)
        {
            ShowCanvasGroup(optionsMenuCanvasGroup, false);
            ShowCanvasGroup(titleMenuCanvasGroup, true);
            ShowCanvasGroup(mainMenuCanvasGroup, true);
        }
        else if (optionsMenuAudio.alpha == 1)
        {
            ShowCanvasGroup(optionsMenuAudio, false);
            ShowCanvasGroup(optionsMenuCanvasGroup, true);
        }
        else if (optionsMenuGraphics.alpha == 1)
        {
            ShowCanvasGroup(optionsMenuGraphics, false);
            ShowCanvasGroup(optionsMenuCanvasGroup, true);
        }
        else // optionsMenuGame
        {
            ShowCanvasGroup(optionsMenuGame, false);
            ShowCanvasGroup(optionsMenuCanvasGroup, true);
        }
    }

    public void OpenAudio()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuAudio, true);
    }

    public void OpenGame()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuGame, true);
    }

    public void OpenGraphics()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuGraphics, true);
    }

    void ShowCanvasGroup(CanvasGroup canvasGroup, bool show)
    {
        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }



}

// Revisar que el interact sigue con el check puesto pero de momento no es ningun problema
// pero hay que corregirlo
