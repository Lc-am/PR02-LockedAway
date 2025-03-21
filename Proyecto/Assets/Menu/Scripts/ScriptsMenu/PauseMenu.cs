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

    //private bool isPaused = false;

    [SerializeField] PlayerControllerNetwork playerControllerNetwork;

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

        if (playerControllerNetwork == null)
        {
            Debug.LogWarning("PlayerControllerNetwork no encontrado en la escena. Asegúrate de que esté presente.");
            return; // Si no se encuentra, no sigas ejecutando el código
        }
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
        if (playerControllerNetwork == null)
        {
            Debug.LogError("PlayerControllerNetwork no está asignado correctamente.");
            return; // Termina la función si no se encuentra el objeto
        }

        if (!playerControllerNetwork.isPaused)
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
        //Time.timeScale = 0;
        playerControllerNetwork.isPaused = true;
        optionsPauseMenu.enabled= true;
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        playerControllerNetwork.isPaused = false;
        optionsPauseMenu.enabled = false;
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        playerControllerNetwork.isPaused = false;
        optionsPauseMenu.enabled = false;
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
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
