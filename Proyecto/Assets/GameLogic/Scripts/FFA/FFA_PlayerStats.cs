//using System;
//using UnityEngine;
//using UnityEngine.Events;

//public class FFA_PlayerStats : MonoBehaviour
//{
//    public int kills;
//    public int deaths;
//    public int autoKills;

//    public UnityEvent onStatsChanged;

//    HumanoidDeath humanoidDeath;
//    HumanoidKiller humanoidKiller;

//    private void Awake()
//    {
//        humanoidDeath = GetComponent<HumanoidDeath>();
//        humanoidKiller = GetComponent<HumanoidKiller>();
//    }

//    private void OnEnable()
//    {
//        humanoidDeath.onDeathOnServer.AddListener(OnDeath);
//        humanoidKiller.onKill.AddListener(OnKill);
//        humanoidKiller.onAutoKill.AddListener(OnAutoKill);
//    }

//    private void OnDisable()
//    {
//        humanoidDeath.onDeathOnServer.RemoveListener(OnDeath);
//        humanoidKiller.onKill.RemoveListener(OnKill);
//        humanoidKiller.onAutoKill.RemoveListener(OnAutoKill);
//    }

//    private void OnDeath()
//    {
//        Debug.Log("FFA_PlayerStats : OnDeath");
//        deaths++;
//        onStatsChanged.Invoke();
//    }

//    private void OnKill(HumanoidDeath humanoidDeath)
//    {
//        Debug.Log("FFA_PlayerStats : OnKill");
//        kills++;
//        onStatsChanged.Invoke();
//    }

//    private void OnAutoKill(HumanoidDeath humanoidDeath)
//    {
//        Debug.Log("FFA_PlayerStats : OnAutoKill");
//        autoKills++;
//        onStatsChanged.Invoke();
//    }

//    public void ResetStats()
//    {
//        Debug.Log("Aaaaaaaaaa");
//        kills = deaths = autoKills = 0;
//        onStatsChanged.Invoke();
//    }

//}
