//using System;
//using System.Collections.Generic;
//using TMPro;
//using Unity.Netcode;
//using UnityEngine;

//public class StatsCanvas : NetworkBehaviour
//{
//    [SerializeField] TextMeshProUGUI playersListUI;
//    [SerializeField] TextMeshProUGUI killsListUI;
//    [SerializeField] TextMeshProUGUI deathsListUI;
//    [SerializeField] TextMeshProUGUI autoKillsListUI;

//    Dictionary<ulong, FFA_PlayerStats> playerStatsCache = new();
//    bool dirty = true;

//    private void Start()
//    {
//    }

//    bool firstUpdate = true;
//    private void Update()
//    {
//        PerformFirstUpdate();

//        if (IsServer && dirty)
//        {
//            dirty = false;

//            string players = "";
//            string kills = "";
//            string deaths = "";
//            string autoKills = "";

//            foreach (NetworkClient nc in NetworkManager.Singleton.ConnectedClients.Values)
//            {
//                FFA_PlayerStats playerStats = playerStatsCache[nc.ClientId];

//                players += $"{nc.ClientId}\n";
//                kills += $"{playerStats.kills}\n";
//                deaths += $"{playerStats.deaths}\n";
//                autoKills += $"{playerStats.autoKills}\n";
//            }

//            SetStatsTexts_ClientRPC(players, kills, deaths, autoKills);
//        }
//    }

//    [Rpc(SendTo.ClientsAndHost)]
//    private void SetStatsTexts_ClientRPC(string players, string kills, string deaths, string autoKills)
//    {
//        playersListUI.text = players;
//        killsListUI.text = kills;
//        deathsListUI.text = deaths;
//        autoKillsListUI.text = autoKills;
//    }

//    private void PerformFirstUpdate()
//    {
//        //Debug.Log("*** PerformFirstUpdate");
//        //Debug.Log($"IsServer: {IsServer}");
//        if (firstUpdate)
//        {
//            firstUpdate = false;
//            Debug.Log("StatsCanvas - Start");
//            if (IsServer)
//            {
//                Debug.Log("StatsCanvas - Start - IsServer");
//                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
//                NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
//                Debug.Log("StatsCanvas - Start - Mother of the Mole");

//                foreach (NetworkClient nc in NetworkManager.Singleton.ConnectedClients.Values)
//                {
//                    OnClientConnected(nc.ClientId);
//                }
//            }
//        }
//    }

//    private void OnClientConnected(ulong clientID)
//    {
//        Debug.Log($"OnClientConnected({clientID})");
//        NetworkClient client = NetworkManager.Singleton.ConnectedClients[clientID];
//        FFA_PlayerStats stats = client.PlayerObject.GetComponentInChildren<FFA_PlayerStats>(true);
//        Debug.Log(stats);
//        stats.onStatsChanged.AddListener(OnStatsChanged);

//        playerStatsCache.Add(clientID, stats);

//        dirty = true;
//    }

//    private void OnClientDisconnected(ulong clientID)
//    {
//        Debug.Log($"OnClientDisconnected({clientID})");
//        NetworkClient client = NetworkManager.Singleton.ConnectedClients[clientID];
//        FFA_PlayerStats stats = client.PlayerObject.GetComponentInChildren<FFA_PlayerStats>(true);
//        stats.onStatsChanged.RemoveListener(OnStatsChanged);

//        playerStatsCache.Remove(clientID);

//        dirty = true;
//    }

//    void OnStatsChanged()
//    {
//        Debug.Log("OnStatsChanged");
//        dirty = true;
//    }
//}
