using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private TMP_Text playersListText;
    [SerializeField] private Button startGameButton;
    [SerializeField] private string GameScene;

    private void Start()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            startGameButton.gameObject.SetActive(true);
            startGameButton.onClick.AddListener(TryStartGame);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }

        UpdatePlayerListUI();
        NetworkManager.Singleton.OnClientConnectedCallback += (_) => UpdatePlayerListUI();
        NetworkManager.Singleton.OnClientDisconnectCallback += (_) => UpdatePlayerListUI();
    }

    void UpdatePlayerListUI()
    {
        string playerList = "Jugadores conectados:\n";
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            playerList += $"- Jugador {client.ClientId}\n";
        }

        playersListText.text = playerList;
    }

    void TryStartGame()
    {
        if (NetworkManager.Singleton.ConnectedClients.Count == 2)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
        }
    }
}
