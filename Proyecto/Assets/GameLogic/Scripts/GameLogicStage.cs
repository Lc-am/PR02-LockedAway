using System;
using Unity.Netcode;
using UnityEngine;

public abstract class GameLogicStage : NetworkBehaviour
{
    protected GameLogic gameLogic;
    internal abstract bool ShouldPlayerBeActive();

    protected virtual void Awake() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }


    public virtual void NotifyActivatedFromPreviousStageOnServer()
    {
    }

    public virtual void NotifyActivatedFromNextStageOnServer()
    {
    }

    public virtual void NotifyDeactivatedOnServer()
    {
    }

    public virtual void Init(GameLogic gameLogic)
    {
        this.gameLogic = gameLogic;
    }

    public virtual void NotifyActivatedOnClient()
    {
    }

    public virtual void NotifyDeactivatedOnClient()
    {
    }
}
