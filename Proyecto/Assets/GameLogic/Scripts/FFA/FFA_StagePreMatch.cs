using DG.Tweening;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Unity.Collections.LowLevel.Unsafe;

public class FFA_StagePreMatch : GameLogicStageWithCanvas
{
    [SerializeField] TextMeshProUGUI playersListUI;
    [SerializeField] Button startButton;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (!IsServer)
        {
            startButton.interactable = false;
        }
    }

    float lastPlayerListingTime = 0f;
    const float timeBetweenPlayerListings = 1f;
    private void Update()
    {
        if (IsServer)
        {
            string playerListText = "";
            if ((Time.time - lastPlayerListingTime) > timeBetweenPlayerListings)
            {
                foreach (NetworkClient cc in NetworkManager.Singleton.ConnectedClients.Values)
                {
                    playerListText += $"{cc.ClientId}\n";
                }
                lastPlayerListingTime = Time.time;

                SetPlayerListText_ClientRPC(playerListText);
            }
        }
    }


    [Rpc(SendTo.ClientsAndHost)]
    private void SetPlayerListText_ClientRPC(string playerListText)
    {
        playersListUI.text = playerListText;
    }

    public void OnButtonStart()
    {
        gameLogic.NextStage();
    }

    public void OnButtonBack()
    {
        gameLogic.PreviousStage();
    }

    internal override bool ShouldPlayerBeActive()
    {
        return false;
    }


}
