using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : NetworkBehaviour
{
    public static GameLogic instance;

    public UnityEvent onActivateBecauseGameLogicStage;
    public UnityEvent onDeactivateBecauseGameLogicStage;

    public bool ShouldBeActiveBecauseGameLogicStage()
    {
        return currentStageIndex.Value != -1 ? 
            allStages[currentStageIndex.Value].ShouldPlayerBeActive() : 
            false;
    }

    GameLogicStage[] allStages;

    NetworkVariable<int> currentStageIndex = new(-1);

    private void Awake()
    {
        allStages = GetComponentsInChildren<GameLogicStage>();
        foreach (GameLogicStage gls in allStages) 
            { gls.Init(this); }
        instance = this;
    }

    private void Start()
    {
        if (IsServer)
        {
            Debug.Log($"IsServer: {IsServer}");
            NextStage();
        }
        else
        {
            allStages[currentStageIndex.Value].NotifyActivatedOnClient();
        }
    }

    public void NextStage()
    {
        Debug.Log("NextStage");
        DeactivateCurrentStage();
        NotifyStageDeactivated_ClientRPC(currentStageIndex.Value);

        currentStageIndex.Value++;
        if (currentStageIndex.Value >= allStages.Length)
        { currentStageIndex.Value = 0; }

        allStages[currentStageIndex.Value].NotifyActivatedFromPreviousStageOnServer();

        Debug.Log($"... {currentStageIndex}");
        InvokeStageActivationEvents();

        NotifyStageActivated_ClientRPC(currentStageIndex.Value);
    }

    public void PreviousStage()
    {
        Debug.Log("PreviousStage");
        DeactivateCurrentStage();
        NotifyStageDeactivated_ClientRPC(currentStageIndex.Value);

        currentStageIndex.Value--;
        if (currentStageIndex.Value < 0)
        {
            // Salir del juego
        }

        if (currentStageIndex.Value >= 0)
        { allStages[currentStageIndex.Value].NotifyActivatedFromNextStageOnServer(); }

        Debug.Log($"... {currentStageIndex.Value}");
        InvokeStageActivationEvents();

        NotifyStageActivated_ClientRPC(currentStageIndex.Value);
    }

    void InvokeStageActivationEvents()
    {
        if (allStages[currentStageIndex.Value].ShouldPlayerBeActive())
        { onActivateBecauseGameLogicStage.Invoke(); }
        else
        { onDeactivateBecauseGameLogicStage.Invoke(); }
    }

    private void DeactivateCurrentStage()
    {
        if (currentStageIndex.Value != -1)
        {
            allStages[currentStageIndex.Value].NotifyDeactivatedOnServer();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    void NotifyStageDeactivated_ClientRPC(int stageIndex)
    {
        if (stageIndex != -1)
        {
            allStages[stageIndex].NotifyDeactivatedOnClient();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    void NotifyStageActivated_ClientRPC(int stageIndex)
    {
        if (stageIndex != -1)
        {
            allStages[stageIndex].NotifyActivatedOnClient();
        }
    }



}
