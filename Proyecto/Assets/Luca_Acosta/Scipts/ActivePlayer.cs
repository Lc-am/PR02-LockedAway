using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class ActivePlayer : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public void aaaaaActivate()
    {
        NetworkManager.Singleton.StartHost();
        canvas.SetActive(false);
    }
}
