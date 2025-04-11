using UnityEngine;
using DG.Tweening;

public abstract class GameLogicStageWithCanvas : GameLogicStage
{
    CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }

    public override void NotifyActivatedFromPreviousStageOnServer()
    {
        base.NotifyActivatedFromPreviousStageOnServer();
    }

    public override void NotifyActivatedFromNextStageOnServer()
    {
        base.NotifyActivatedFromNextStageOnServer();
    }

    public override void NotifyActivatedOnClient()
    {
        base.NotifyActivatedOnClient();
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(1f, 0.25f);
    }

    public override void NotifyDeactivatedOnClient()
    {
        base.NotifyDeactivatedOnClient();
        canvasGroup.DOFade(0f, 0.25f).OnComplete(() => canvasGroup.gameObject.SetActive(false));
    }

    public override void NotifyDeactivatedOnServer()
    {
        base.NotifyDeactivatedOnServer();
    }
}
