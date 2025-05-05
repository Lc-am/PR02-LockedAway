using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Network : NetworkBehaviour
{
    [SerializeField] TMP_InputField hostAddress;
    string hostAddressString;

    public void CreateLobby()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void JoinLobby()
    {
        hostAddressString = hostAddress.text;
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(hostAddressString, 7777);
        NetworkManager.Singleton.StartClient();
    }
}
