using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class FFA_StagePostMatch : GameLogicStageWithCanvas
{
    [SerializeField] Button restartButton;

    internal override bool ShouldPlayerBeActive()
    {
        return false;
    }

    private void Start()
    {
        if (!IsServer)
        {
            restartButton.interactable = false;
        }
    }


    public void OnRestartButton()
    {
        // Codigo para resetear la partida
        gameLogic.NextStage();

        Debug.Log("OnRestartButton");


        // No tenemos stats

        //foreach (NetworkClient nc in NetworkManager.ConnectedClients.Values)
        //{
        //    nc.PlayerObject.GetComponentInChildren<FFA_PlayerStats>(true).ResetStats();
        //    Debug.Log($"OnRestartButton... {nc}");
        //}
    }
}
