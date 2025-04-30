using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;
using Unity.Netcode;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference skipAction;
    [SerializeField] private InputActionReference escapeAction;

    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject continueText;
    private bool buttonsShown = false;

    [SerializeField] private Canvas Canvas;

    [Header("Play")]
    [SerializeField] private Button playButton;

    [SerializeField] private CanvasGroup playSelectHostOrClient;

    [SerializeField] private Button playSelectHost;
    [SerializeField] private Button playSelectClient;

    [SerializeField] private CanvasGroup createLobbyCanvasGroup;
    [SerializeField] private CanvasGroup joinLobbyCanvasGroup;

    [Header("Options")]
    [SerializeField] private Button optionsButton;
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private CanvasGroup optionsMenuCanvasGroup;
    [SerializeField] private CanvasGroup titleMenuCanvasGroup;

    [SerializeField] private Button optionsAudio;
    [SerializeField] private Button optionsGame;
    [SerializeField] private Button optionsGraphics;

    [SerializeField] private CanvasGroup optionsMenuAudio;
    [SerializeField] private CanvasGroup optionsMenuGame;
    [SerializeField] private CanvasGroup optionsMenuGraphics;

    [Header("Others")]
    [SerializeField] private Button exitButton;

    [SerializeField] GameObject networkManager;

    private int inScreen;       //1 = Play | 2 = Play - Host | 3 = Play - Client | 4 = Options | 5 = OptionsAudio | 6 = OptionsGame | 7 = OptionsGraphics | 0 = MainMenu
    private bool selectedHost;


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

        playSelectHost.onClick.AddListener(OpenCreateLobby);
        playSelectClient.onClick.AddListener(OpenJoinLobby);

        optionsButton.onClick.AddListener(OpenOptionsMenu);

        optionsAudio.onClick.AddListener(OpenAudio);
        optionsGame.onClick.AddListener(OpenGame);
        optionsGraphics.onClick.AddListener(OpenGraphics);

        exitButton.onClick.AddListener(exitButtonClick);
    }

    void OnDisable()
    {
        skipAction.action.Disable();
        escapeAction.action.Disable();

        escapeAction.action.performed -= goBack;

        playButton.onClick.RemoveListener(playLevel);

        playSelectHost.onClick.RemoveListener(OpenCreateLobby);
        playSelectClient.onClick.RemoveListener(OpenJoinLobby);

        optionsButton.onClick.RemoveListener(OpenOptionsMenu);

        optionsAudio.onClick.RemoveListener(OpenAudio);
        optionsGame.onClick.RemoveListener(OpenGame);
        optionsGraphics.onClick.RemoveListener(OpenGraphics);

        exitButton.onClick.RemoveListener(exitButtonClick);
    }

    void MainMenuCanvas()
    {
        ShowCanvasGroup(mainMenuCanvasGroup, true);
    }

    void playLevel() 
    {
        /*UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");*/ // REVISAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAR
        ShowCanvasGroup(playSelectHostOrClient, true);
        ShowCanvasGroup(mainMenuCanvasGroup, false);
        ShowCanvasGroup(titleMenuCanvasGroup, false);
        inScreen = 1;
    }

    void OpenCreateLobby()
    {
        NetworkManager.Singleton.StartHost();
        selectedHost = true;
        ShowCanvasGroup(createLobbyCanvasGroup, true);
        ShowCanvasGroup(playSelectHostOrClient, false);
        inScreen = 2;
    }

    void OpenJoinLobby()
    {
        NetworkManager.Singleton.StartClient();
        selectedHost = false;
        ShowCanvasGroup(joinLobbyCanvasGroup, true);
        ShowCanvasGroup(playSelectHostOrClient, false);
        inScreen = 3;
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

        inScreen = 4;
    }

    public void OpenAudio()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuAudio, true);

        inScreen = 5;
    }

    public void OpenGame()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuGame, true);

        inScreen = 6;
    }

    public void OpenGraphics()
    {
        ShowCanvasGroup(optionsMenuCanvasGroup, false);
        ShowCanvasGroup(optionsMenuGraphics, true);

        inScreen = 7;
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
                ShowCanvasGroup(playSelectHostOrClient, false);
                ShowCanvasGroup(mainMenuCanvasGroup, true);
                ShowCanvasGroup(titleMenuCanvasGroup, true);
                inScreen = 0;
                break;
            case 2:
                ShowCanvasGroup(createLobbyCanvasGroup, false);
                ShowCanvasGroup(playSelectHostOrClient, true);
                NetworkManager.Singleton.Shutdown(true);
                inScreen = 1;
                break;
            case 3:
                ShowCanvasGroup(joinLobbyCanvasGroup, false);
                ShowCanvasGroup(playSelectHostOrClient, true);
                NetworkManager.Singleton.Shutdown(true);
                inScreen = 1;
                break;
            case 4:
                ShowCanvasGroup(optionsMenuCanvasGroup, false);
                ShowCanvasGroup(titleMenuCanvasGroup, true);
                ShowCanvasGroup(mainMenuCanvasGroup, true);
                inScreen = 0;
                break;
            case 5:
                ShowCanvasGroup(optionsMenuCanvasGroup, true);
                ShowCanvasGroup(optionsMenuAudio, false);
                inScreen = 4;
                break;
            case 6:
                ShowCanvasGroup(optionsMenuCanvasGroup, true);
                ShowCanvasGroup(optionsMenuGame, false);
                inScreen = 4;
                break;
            case 7:
                ShowCanvasGroup(optionsMenuCanvasGroup, true);
                ShowCanvasGroup(optionsMenuGraphics, false);
                inScreen = 4;
                break;
        }
    }
}
