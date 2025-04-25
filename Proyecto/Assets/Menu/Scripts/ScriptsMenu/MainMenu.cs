using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference skipAction;
    [SerializeField] private InputActionReference escapeAction;

    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject continueText;
    private bool buttonsShown = false;

    [SerializeField] private Canvas Canvas;
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private CanvasGroup optionsMenuCanvasGroup;
    [SerializeField] private CanvasGroup titleMenuCanvasGroup;

    [SerializeField] private Button optionsAudio;
    [SerializeField] private Button optionsGame;
    [SerializeField] private Button optionsGraphics;

    [SerializeField] private CanvasGroup optionsMenuAudio;
    [SerializeField] private CanvasGroup optionsMenuGame;
    [SerializeField] private CanvasGroup optionsMenuGraphics;

    [SerializeField] private CanvasGroup Lobby;

    private int inScreen;       //1 = Play | 2 = Options | 3 = OptionsAudio | 4 = OptionsGame | 5 = OptionsGraphics | 0 = MainMenu

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
        escapeAction.action.Enable();

        escapeAction.action.performed += goBack;

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
        escapeAction.action.Disable();

        escapeAction.action.performed -= goBack;

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
        /*UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");*/ // REVISAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAR
        ShowCanvasGroup(Lobby, true);
        ShowCanvasGroup(mainMenuCanvasGroup, false);
        ShowCanvasGroup(titleMenuCanvasGroup, false);
        inScreen = 1;
    }

    //public void CloseLobby()
    //{
    //    ShowCanvasGroup(Lobby, false);
    //    ShowCanvasGroup(titleMenuCanvasGroup, true);
    //    ShowCanvasGroup(mainMenuCanvasGroup, true);
    //}

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

        inScreen = 2;
    }

    //public void CloseOptions()
    //{
    //    if(optionsMenuCanvasGroup.alpha == 1)
    //    {
    //        ShowCanvasGroup(optionsMenuCanvasGroup, false);
    //        ShowCanvasGroup(titleMenuCanvasGroup, true);
    //        ShowCanvasGroup(mainMenuCanvasGroup, true);
    //    }
    //    else if (optionsMenuAudio.alpha == 1)
    //    {
    //        ShowCanvasGroup(optionsMenuAudio, false);
    //        ShowCanvasGroup(optionsMenuCanvasGroup, true);
    //    }
    //    else if (optionsMenuGraphics.alpha == 1)
    //    {
    //        ShowCanvasGroup(optionsMenuGraphics, false);
    //        ShowCanvasGroup(optionsMenuCanvasGroup, true);
    //    }
    //    else // optionsMenuGame
    //    {
    //        ShowCanvasGroup(optionsMenuGame, false);
    //        ShowCanvasGroup(optionsMenuCanvasGroup, true);
    //    }
    //}

    public void OpenAudio()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuAudio, true);

        inScreen = 3;
    }

    public void OpenGame()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuGame, true);

        inScreen = 4;
    }

    public void OpenGraphics()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuGraphics, true);

        inScreen = 5;
    }

    void ShowCanvasGroup(CanvasGroup canvasGroup, bool show)
    {
        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }

    private void goBack(InputAction.CallbackContext context)
    {
        switch(inScreen)
        {
            case 1:
                ShowCanvasGroup(Lobby, false);
                ShowCanvasGroup(mainMenuCanvasGroup, true);
                ShowCanvasGroup(titleMenuCanvasGroup, true);
                inScreen = 0;
                break;
            case 2:
                ShowCanvasGroup(optionsMenuCanvasGroup, false);
                ShowCanvasGroup(titleMenuCanvasGroup, true);
                ShowCanvasGroup(mainMenuCanvasGroup, true);
                inScreen = 0;
                break;
            case 3:
                ShowCanvasGroup(optionsMenuCanvasGroup, true);
                ShowCanvasGroup(optionsMenuAudio, false);
                inScreen = 2;
                break;
            case 4:
                ShowCanvasGroup(optionsMenuCanvasGroup, true);
                ShowCanvasGroup(optionsMenuGame, false);
                inScreen = 2;
                break;
            case 5:
                ShowCanvasGroup(optionsMenuCanvasGroup, true);
                ShowCanvasGroup(optionsMenuGraphics, false);
                inScreen = 2;
                break;
        }
    }
}
