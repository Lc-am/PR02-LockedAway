using System.Drawing.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkScenarioManager : NetworkBehaviour
{
    [Header("Scenarios")]
    [SerializeField] private GameObject splashGameScenarioPrefab;
    //[SerializeField] private GameObject spaceShipScenarioPrefab;
    //[SerializeField] private GameObject castleScenarioPrefab;
    [SerializeField] private GameObject insideCastleScenarioPrefab;

    [Space]
    [SerializeField] private GameObject spawnPoint;

    private GameObject[] players;
    public bool inGame = false;

    public void StartSplashGameScenario()
    {
        splashGameScenarioPrefab.SetActive(true);
        //spaceShipScenarioPrefab.SetActive(false);
        //castleScenarioPrefab.SetActive(false);
        insideCastleScenarioPrefab.SetActive(false);
        SetScenarioStateClientRpc(0);
        inGame = false;
    }

    public void StartSpaceShipScenario()
    {
        splashGameScenarioPrefab.SetActive(false);
        //spaceShipScenarioPrefab.SetActive(true);
        //castleScenarioPrefab.SetActive(false);
        insideCastleScenarioPrefab.SetActive(false);
        inGame = true;
    }

    public void StartCastleScenario()
    {
        splashGameScenarioPrefab.SetActive(false);
        //spaceShipScenarioPrefab.SetActive(false);
        //castleScenarioPrefab.SetActive(true);
        insideCastleScenarioPrefab.SetActive(false);
        inGame = true;
    }

    public void StartInsideCastleScenario()
    {
        splashGameScenarioPrefab.SetActive(false);
        //spaceShipScenarioPrefab.SetActive(false);
        //castleScenarioPrefab.SetActive(false);
        insideCastleScenarioPrefab.SetActive(true);
        SetScenarioStateClientRpc(3);
        inGame = true;
    }

    [ClientRpc]
    private void SetScenarioStateClientRpc(int scenarioIndex)
    {
        splashGameScenarioPrefab.SetActive(false);
        //spaceShipScenarioPrefab.SetActive(false);
        //castleScenarioPrefab.SetActive(false);
        insideCastleScenarioPrefab.SetActive(false);

        switch (scenarioIndex)
        {
            case 0:
                splashGameScenarioPrefab.SetActive(true);
                break;
            case 1:
                //spaceShipScenarioPrefab.SetActive(true);
                break;
            case 2:
                //castleScenarioPrefab.SetActive(true);
                break;
            case 3:
                insideCastleScenarioPrefab.SetActive(true);
                break;
        }
    }
}