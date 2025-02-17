using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public InputActionReference pauseAction;

    public Button resumeButton;
    public Button optionsButton;
    public Button mainMenuButton;

    public Canvas optionsMenuCanvas;

    public Canvas optionsPauseMenu;
    private bool isPaused = false;

    private void OnEnable()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += TogglePause; 
    }

    public void OnDisable()
    {
        pauseAction.action.Disable();
        pauseAction.action.performed -= TogglePause;
    }

    public void Start()
    {
        pauseAction.action.Enable();
        optionsPauseMenu = GetComponent<Canvas>();
        optionsPauseMenu.enabled = false;

        //gameObject.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        optionsButton.onClick.AddListener(OpenOptions);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            OpenPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        isPaused = true;
        optionsPauseMenu.enabled= true;
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        optionsPauseMenu.enabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        optionsPauseMenu.enabled = false;
    }

    public void OpenOptions()
    {
        optionsPauseMenu.enabled = false;
        optionsMenuCanvas.enabled = true;
    }

    public void CloseOptions()
    {
        optionsPauseMenu.enabled = true;
        optionsMenuCanvas.enabled = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainMenu");
    }
}
