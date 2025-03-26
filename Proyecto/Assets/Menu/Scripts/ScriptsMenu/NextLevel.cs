using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public InputActionReference switchSceneAction;   

    private void OnEnable()
    {
        switchSceneAction.action.Enable();
        switchSceneAction.action.performed += ChangeScene;
    }

    private void OnDisable()
    {
        switchSceneAction.action.Disable();
        switchSceneAction.action.performed -= ChangeScene;
    }

    private void ChangeScene(InputAction.CallbackContext context)
    {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Jugable2");
    }
}
