using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerPauseMenu : NetworkBehaviour
{
    [SerializeField] private CanvasGroup pauseMenuCanvasGroup;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitGameButton;

    private bool isPaused = false;
    private PlayerControllerNetwork playerController;

    private void Start()
    {
        if (!IsLocalPlayer)
        {
            gameObject.SetActive(false);
            return;
        }

        playerController = GetComponent<PlayerControllerNetwork>();

        resumeButton.onClick.AddListener(TogglePauseMenu);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        exitGameButton.onClick.AddListener(ExitGame);

        ShowPauseMenu(false);
    }

    private void Update()
    {
        if (!IsLocalPlayer) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;
        ShowPauseMenu(isPaused);

        // (Opcional) congelar movimiento local
        if (playerController != null)
            playerController.enabled = !isPaused;
    }

    private void ShowPauseMenu(bool show)
    {
        pauseMenuCanvasGroup.alpha = show ? 1 : 0;
        pauseMenuCanvasGroup.interactable = show;
        pauseMenuCanvasGroup.blocksRaycasts = show;

        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = show;
    }

    private void ReturnToMainMenu()
    {
        if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.Shutdown();
        }

        SceneManager.LoadScene("MainMenu");
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
