using System.Drawing.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Scene[] scenes;

    private Scene currentScene;

    private GameObject[] players;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}