using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CompanySplashController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string nextSceneName = "Inicio"; // Nombre de la escena de inicio

    void Start()
    {
        if (videoPlayer != null)
        {
            // Se suscribe al evento que se dispara cuando el video termina
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Cambia a la siguiente escena al terminar el video
        SceneManager.LoadScene(nextSceneName);
    }
}
