using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetWorkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button serverButton;
    [SerializeField] private Button clientButton;

    //[SerializeField] private NetworkManager networkManager;

    public void OnClickHostButton()
    {
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
    }
}
