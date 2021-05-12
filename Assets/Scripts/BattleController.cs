using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EBattleStage {Intro, PlayerTurn, PlayeMove, EnemyTurn, EnemyMove, Conclusion }

//Battle Controller works as a global event triggerer for battle scene
public class BattleController : MonoBehaviour
{
    public static BattleController Singleton { get; private set; }
    private EBattleStage _currentBattleStage;

    public event Action OnIntro;
    public event Action OnPlayerTurn;
    public event Action OnPlayerMove;
    public event Action OnEnemyTurn;
    public event Action OnEnemyMove;
    public event Action OnConclusion;

    private void Awake()
    {
        if (Singleton != null) Destroy(this.gameObject);

        Singleton = this;
        DontDestroyOnLoad(this);
        SetBattleStage(EBattleStage.Intro);
    }

    public void SetBattleStage(EBattleStage nextStage)
    {
        _currentBattleStage = nextStage;

        switch (_currentBattleStage)
        {
            case EBattleStage.Intro:
                OnIntro?.Invoke();
                break;
            case EBattleStage.PlayerTurn:
                OnPlayerTurn?.Invoke();
                break;
            case EBattleStage.PlayeMove:
                OnPlayerMove?.Invoke();
                break;
            case EBattleStage.EnemyTurn:
                OnEnemyTurn?.Invoke();
                break;
            case EBattleStage.EnemyMove:
                OnEnemyMove?.Invoke();
                break;
            case EBattleStage.Conclusion:
                OnConclusion?.Invoke();
                break;
            default:
                Debug.LogWarning("COWABUNGA IT IS!");//Something very wrong just happened
                break;
        }
    }

    public void RemoveEvents()
    {
        OnIntro = null;
        OnPlayerTurn = null;
        OnPlayerMove = null;
        OnEnemyTurn = null;
        OnEnemyMove = null;
        OnConclusion = null;
    }

    
}
