using System.Drawing.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : NetworkBehaviour
{
    [Header("Scenarios")]
    [SerializeField] private GameObject splashGameScenarioPrefab;
    //[SerializeField] private GameObject spaceShipScenarioPrefab;
    //[SerializeField] private GameObject castleScenarioPrefab;
    [SerializeField] private GameObject insideCastleScenarioPrefab;

    [Space]
    [SerializeField] private GameObject spawnPoint;

    private GameObject[] players;

    private void Awake()
    {
        GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < foundPlayers.Length; i++)
        {
            players[i] = foundPlayers[i];
        }
    }

    void spawnPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnPoint.transform.position;
        }
    }

    public void StartSplashGameScenario()
    {
        splashGameScenarioPrefab.SetActive(true);
        //spaceShipScenarioPrefab.SetActive(false);
        //castleScenarioPrefab.SetActive(false);
        insideCastleScenarioPrefab.SetActive(false);
    }

    public void StartSpaceShipScenario()
    {
        splashGameScenarioPrefab.SetActive(false);
        //spaceShipScenarioPrefab.SetActive(true);
        //castleScenarioPrefab.SetActive(false);
        insideCastleScenarioPrefab.SetActive(false);
    }

    public void StartCastleScenario()
    {
        splashGameScenarioPrefab.SetActive(false);
        //spaceShipScenarioPrefab.SetActive(false);
        //castleScenarioPrefab.SetActive(true);
        insideCastleScenarioPrefab.SetActive(false);
    }

    public void StartInsideCastleScenario()
    {
        splashGameScenarioPrefab.SetActive(false);
        //spaceShipScenarioPrefab.SetActive(false);
        //castleScenarioPrefab.SetActive(false);
        insideCastleScenarioPrefab.SetActive(true);
        spawnPlayers();
    }
}