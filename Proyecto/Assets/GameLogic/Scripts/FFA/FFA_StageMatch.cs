using System;
using DG.Tweening;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class FFA_StageMatch : GameLogicStageWithCanvas
{
    [SerializeField] TextMeshProUGUI remainingTimeUGUI;
    [SerializeField] InputActionReference showStats;
    [SerializeField] CanvasGroup canvasGroupStats;
    [SerializeField] float matchTime = 3f * 60f;

    [Header("Debug")]
    [SerializeField] bool debugEndGame;

    NetworkVariable <float> remainingTime = new();

    private void OnValidate()
    {
        if (debugEndGame)
        {
            debugEndGame = false;
            gameLogic.NextStage();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        canvasGroupStats.alpha = 0f;
        remainingTime.Value = matchTime;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        showStats.action.Enable();
        showStats.action.started += OnToggleStats;
        showStats.action.canceled += OnToggleStats;

        remainingTime.OnValueChanged += OnRemainingTimeChanged;
    }

    internal override bool ShouldPlayerBeActive()
    {
        return true;
    }

    private void Update()
    {
        if (IsServer && isActive)
        {
            if (remainingTime.Value >= 0f)
            {
                remainingTime.Value -= Time.deltaTime;
                if (remainingTime.Value < 0f)
                   { gameLogic.NextStage(); }
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        showStats.action.Disable();
        showStats.action.started -= OnToggleStats;
        showStats.action.canceled -= OnToggleStats;

        remainingTime.OnValueChanged -= OnRemainingTimeChanged;
    }

    private void OnToggleStats(InputAction.CallbackContext context)
    {
        bool isPressed = context.action.ReadValue<float>() > 0.5f;
        canvasGroupStats.DOFade(isPressed ? 1f : 0f, 0.25f);
    }

    void OnRemainingTimeChanged(float previousTime, float newTime)
    {
        remainingTimeUGUI.text = "" + Mathf.Abs(newTime);
    }

    bool isActive;
    public override void NotifyActivatedFromPreviousStageOnServer()
    {
        isActive = true;
    }

    public override void NotifyDeactivatedOnServer()
    {
        isActive = false;
        remainingTime.Value = matchTime;
    }
}
