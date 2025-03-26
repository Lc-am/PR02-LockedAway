using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using System.Collections.Generic;



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

    private Lobby hostLobby;
    private float heartbeatTimer;

    async void Start()
    {
        await  UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        HandleLobbyHeartbeat();
    }

    async void HandleLobbyHeartbeat()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if(heartbeatTimer < 0f)
            {
                float heartbeatTimerMax = 15f;
                heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }

    async void CreateLobby()
     {
        try
        {
            string lobbyName = "myLobby";
            int maxPlayers = 4;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            hostLobby = lobby;

            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.MaxPlayers);
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
        }
     }

    private async void ListLobbies()
    {
        try 
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };
            
            
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found: " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers) ;
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    private async void JoinLobby()
    {
        try
        {
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync();

            await LobbyService.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }
}
