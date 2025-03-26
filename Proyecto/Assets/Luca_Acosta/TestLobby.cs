using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;



public class TestLobby : MonoBehaviour
{
    //async void Start()
    //{
    //    await UnityServices.InitializeAsync();
        
    //    if(UnityServices.State == ServicesInitializationState.Initialized)
    //    {
    //        AuthenticationService.Instance.SignedIn += OnSignedIn;

    //        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    //        if(AuthenticationService.Instance.IsSignedIn)
    //        {
    //            string username = PlayerPrefs.GetString(key: "Username");
    //            if(username == "")
    //            {
    //                username = "Player";
    //                PlayerPrefs.GetString("Username", username);
    //            }
    //        }

    //        SceneManager.LoadSceneAsync("Splash Company");
    //    }
    //}

    //private void OnSignedIn()
    //{
    //    Debug.Log(message: $"Player Id:  {AuthenticationService.Instance.PlayerId}");
    //    Debug.Log(message: $"Token:  {AuthenticationService.Instance.AccessToken}");
    //}

    async void Start()
    {
        await  UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += ()
            =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "myLobby";
            int maxPlayers = 4;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);

            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.MaxPlayers);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    //private async void ListLobbies()
    //{
    //    try
    //    {
             
    //    }
    //}
}
