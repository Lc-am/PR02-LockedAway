//using DG.Tweening;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using UnityEditor;


//public class LoadingScene : MonoBehaviour
//{
//    [SerializeField] string firstSceneToLoad;
//    [SerializeField] CanvasGroup fader;
//    Scene currentScene;

//    static public LoadingScene instance;
//    static bool wasLoadedOnPlayModeStateChange;


//#if UNITY_EDITOR
//    [InitializeOnLoadMethod]

//    static void ListenToAppliactionModeChange()
//    {
//        EditorApplication.playmodeStateChanged += OnPlayModeStateChange;
//    }

//    private static void OnPlayModeStateChange(PlayModeStateChange change)
//    {
//        if (change == PlayModeStateChange.EnteredPlayMode)
//        {
//            wasLoadedOnPlayModeStateChange = false;

//            if (SceneManager.GetSceneAt(0).name != "LoadingScene")
//            {
//                SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);
//                wasLoadedOnPlayModeStateChange = true;
//                Scene loading = SceneManager.GetSceneAt(SceneManager.loadedSceneCount - 1);
//            }
//        }
//    }
//#endif
//    public void LoadScene(string sceneName)
//    {
//        StartCoroutine(LoadSceneCoroutine(sceneName));
//    }

//    private void Start()
//    {
//        if (!wasLoadedOnPlayModeStateChange)
//        {
//            LoadScene(firstSceneToLoad);
//        }
//        else
//        {
//            currentScene = SceneManager.GetSceneAt(0);
//            fader.DoFade(0f, 0f);
//        }
//    }

//    private void Awake()
//    {
//        instance = this;
//    }

//    public IEnumerator LoadSceneCoroutine(string sceneName)
//    {
//        // Fade
//        {
//            Tween fadeTween = fader.DOFade(1f, 1f);
//            do
//            {
//                yield return new();
//            }
//            while (fadeTween.IsPlaying());
//        }

//        // Descargar la escena actual
//        if (currentScene.isLoaded)
//        {
//            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
//            do
//            {
//                yield return new();
//            }
//            while (!unloadOperation.isDone);
//        }

//        //Para que tenga un Tiempo Determinado
//        float timeBeforeLoad = Time.realtimeSinceStartup;

//        //Cargar la escena
//        {
//            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
//            do
//            {
//                yield return new();
//            }
//            while (!loadOperation.isDone);

//            currentScene = SceneManager.GetSceneAt(SceneManager.loadedSceneCount - 1);
//            SceneManager.SetActiveScene(currentScene);
//        }

//        //Continuacion Tiempo Determinado
//        float timeElapsedLoading = Time.realtimeSinceStartup - timeBeforeLoad;
//        if (timeElapsedLoading < 3f)
//        {
//            yield return new WaitForSeconds(3f - timeElapsedLoading);
//        }

//        //Fade
//        {
//            Tween fadeTween = fader.DOFade(0f, 1f);
//            do
//            {
//                yield return new();
//            }
//            while (fadeTween.IsPlaying());
//        }
//    }

//#if UNITY_EDITOR
//    [MenuItem("LoadingScene/Debug/Change to GameScene")]
//    [MenuItem("LoadingScene/Debug/Change to Ejercicio1Scene")]

//    static public void DebugChangeToMainMenuScene()
//    {
//        LoadingScene.instance.LoadScene("Game");
//    }

//    static public void DebugChangeToEjercicio1Scene()
//    {
//        LoadingScene.instance.LoadScene("Ejercicio1");
//    }
//#endif
//}
