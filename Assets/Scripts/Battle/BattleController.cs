using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EBattleStage {Intro, PlayerTurn, PlayerMove, EnemyTurn, EnemyMove, Conclusion }

//Battle Controller works as a global event triggerer for battle scene
public class BattleController : MonoBehaviour
{
    public static BattleController Instance { get; private set; }
    private EBattleStage _currentBattleStage;
    public EBattleStage GetCurrentBattleStage() => _currentBattleStage;

    public event Action OnIntro;
    public event Action OnPlayerTurn;
    public event Action OnPlayerMove;
    public event Action OnEnemyTurn;
    public event Action OnEnemyMove;
    public event Action OnConclusion;

    [Header("BATTLE INFORMATION")]
    public float hypeBarValue = 0;
    public int correctInputCombo = 0;

    //LATER WILL ALSO GET PLAYER AND ENEMY MAX HEALTH FROM SCRIPTABLE OBJECTS AND APPLY CURRENT HEALTH TO THEM ON START
    public float playercurrenthealth = 100;
    public float enemycurrenthealth = 100;
    /////////////////
    public event Action OnUIUpdate;

    [HideInInspector]
    public RythmMove currentMove = null;
    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);

        Instance = this;
    }

    public void SetBattleStage(EBattleStage nextStage, RythmMove move = null)
    {
        _currentBattleStage = nextStage;
        if(move != null)
        {
            currentMove = move;
        }
        switch (_currentBattleStage)
        {
            case EBattleStage.Intro:
                Debug.Log("Battle Intro");
                OnIntro?.Invoke();
                break;
            case EBattleStage.PlayerTurn:
                Debug.Log("Player Turn");
                OnUIUpdate?.Invoke();
                OnPlayerTurn?.Invoke();
                break;
            case EBattleStage.PlayerMove:
                Debug.Log("Player Move");
                correctInputCombo = 0;                
                OnPlayerMove?.Invoke();
                break;
            case EBattleStage.EnemyTurn:
                Debug.Log("Enemy Turn");
                OnUIUpdate?.Invoke();
                OnEnemyTurn?.Invoke();
                StartCoroutine(WaitToChangeStage(EBattleStage.EnemyMove));
                break;
            case EBattleStage.EnemyMove:
                Debug.Log("Enemy Move");
                correctInputCombo = 0;               
                OnEnemyMove?.Invoke();
                StartCoroutine(WaitToChangeStage(EBattleStage.PlayerTurn));
                break;
            case EBattleStage.Conclusion:
                Debug.Log("Battle End");
                OnConclusion?.Invoke();
                break;
            default:
                Debug.LogWarning("COWABUNGA IT IS!");//Something very wrong just happened
                break;
        }
    }

    public void UpdateHype(bool playerTurn, bool hit)
    {
        if(playerTurn)
        {
            if(hit)
            {
                if(correctInputCombo > 4)
                {
                    hypeBarValue += 0.02f;
                }
                else
                {
                    hypeBarValue+= 0.01f;
                }
                correctInputCombo++;
            }
            else
            {
                hypeBarValue -= 0.05f;
                correctInputCombo = 0;
            }
        }
        else//enemy attack
        {
            if (hit)
            {
                if (correctInputCombo > 4)
                {
                    hypeBarValue -= 0.02f;
                }
                else
                {
                    hypeBarValue -= 0.01f;
                }
                correctInputCombo++;
            }
            else
            {
                hypeBarValue += 0.05f;
                correctInputCombo = 0;
            }
        }
        hypeBarValue = Mathf.Clamp(hypeBarValue, 0f, 1f);
        OnUIUpdate?.Invoke();
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

    IEnumerator WaitToChangeStage(EBattleStage newStage, float time = 3)
    {
        yield return new WaitForSeconds(time);
        SetBattleStage(newStage);
    }

    
}
