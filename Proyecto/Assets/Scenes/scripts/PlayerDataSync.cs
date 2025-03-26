using Unity.Netcode;
using UnityEngine;

public class PlayerDataSync : NetworkBehaviour
{
    public NetworkVariable<Vector3> playerPosition = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        if (IsServer) // Solo el servidor puede cargar datos
        {
            LoadPlayerData();
        }
    }

    private void LoadPlayerData()
    {
        if (SaveLoadManager.Instance == null)
        {
            Debug.LogError("SaveLoadManager no encontrado.");
            return;
        }

        GameData savedData = SaveLoadManager.Instance.LoadGame();
        if (savedData != null)
        {
            playerPosition.Value = savedData.playerPosition;
            transform.position = savedData.playerPosition; // Aplicar posición al jugador
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SavePlayerDataServerRpc(Vector3 newPosition)
    {
        if (IsServer)
        {
            GameData data = new GameData { playerPosition = newPosition };
            SaveLoadManager.Instance.SaveGame(data);
            Debug.Log("Datos guardados en el servidor.");
        }
    }
}
