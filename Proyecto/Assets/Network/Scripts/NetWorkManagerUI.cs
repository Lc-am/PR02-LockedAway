using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class NetWorkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button serverButton;
    [SerializeField] private Button clientButton;
    bool host = false;

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
            host = true;
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
    }

    private void Update()
    {
        if(host)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("mainScene");
        }
    }
}
