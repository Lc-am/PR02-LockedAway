using System.Collections.Generic;
using System;
using Unity.Netcode;
using UnityEngine;

public class SpawnPoint : NetworkBehaviour
{
    public GameObject playerPrefab;

    private Dictionary<ulong, Vector3> spawnPositions = new Dictionary<ulong, Vector3>
    {
        { 0, new Vector3(26.34769f, 1.844913f, -120.8736f) },     // Host
        //{ 1, new Vector3(5, 0, 0) },     // Cliente 1
        //{ 2, new Vector3(10, 0, 0) },    // Cliente 2
    };

    private void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;

            // También crear el personaje del host
            ulong hostClientId = NetworkManager.Singleton.LocalClientId;
            SpawnPlayer(hostClientId);
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
            return; // El host ya fue instanciado en Start()

        SpawnPlayer(clientId);
    }

    private void SpawnPlayer(ulong clientId)
    {
        if (spawnPositions.TryGetValue(clientId, out Vector3 spawnPos))
        {
            GameObject playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }
        else
        {
            Debug.LogWarning($"No hay posición asignada para el cliente {clientId}");
        }
    }
}
