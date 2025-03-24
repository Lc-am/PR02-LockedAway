using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;

public class TestLobby : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
        
        if(UnityServices.State == ServicesInitializationState.Initialized)
        {
            AuthenticationService.Instance.SignedIn += OnSignedIn;

            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if(AuthenticationService.Instance.IsSignedIn)
            {
                string username = PlayerPrefs.GetString(key: "Username");
                if(username == "")
                {
                    username = "Player";
                    PlayerPrefs.GetString("Username", username);
                }
            }

            SceneManager.LoadSceneAsync("Splash Company");
        }
    }

    private void OnSignedIn()
    {
        Debug.Log(message: $"Player Id:  {AuthenticationService.Instance.PlayerId}");
        Debug.Log(message: $"Token:  {AuthenticationService.Instance.AccessToken}");
    }
}
