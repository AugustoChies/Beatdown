using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EBattleStage {Intro, PlayerTurn, PlayerMove, EnemyTurn, EnemyMove, Conclusion, DamageStep }

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
    public event Action OnDamage;
    public event Action OnConclusion;

    [SerializeField]
    private PauseController pauseController = null;

    [Header("BATTLE INFORMATION")]
    public float hypeBarValue = 0;
    public int correctInputCombo = 0;

    //LATER WILL ALSO GET PLAYER AND ENEMY MAX HEALTH FROM SCRIPTABLE OBJECTS AND APPLY CURRENT HEALTH TO THEM ON START
    public float playercurrenthealth = 100;
    public float enemycurrenthealth = 100;
    /////////////////   
    private bool _lastToMoveIsPlayer = true;
    public bool battleWinnerPlayer = false;
    public int currentmoveScore = 0;
    public RythmMove enemyMove = null;
    [HideInInspector]
    public RythmMove currentMove = null;

    public event Action<bool,bool> OnUIUpdate;

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
                pauseController.CanPause = false;
                OnIntro?.Invoke();
                break;
            case EBattleStage.PlayerTurn:
                Debug.Log("Player Turn");
                pauseController.CanPause = true;                
                OnPlayerTurn?.Invoke();
                break;
            case EBattleStage.PlayerMove:
                Debug.Log("Player Move");
                _lastToMoveIsPlayer = true;
                OnPlayerMove?.Invoke();
                break;
            case EBattleStage.EnemyTurn:
                Debug.Log("Enemy Turn");
                pauseController.CanPause = true;                
                OnEnemyTurn?.Invoke();
                StartCoroutine(WaitToEnemyMove());
                break;
            case EBattleStage.EnemyMove:
                Debug.Log("Enemy Move");
                _lastToMoveIsPlayer = false;                
                OnEnemyMove?.Invoke();
                break;
            case EBattleStage.DamageStep:
                Debug.Log("DamageStep");                
                OnDamage?.Invoke();
                StartCoroutine(DelayedDamage());
                break;
            case EBattleStage.Conclusion:
                Debug.Log("Battle End");
                pauseController.CanPause = false;
                OnConclusion?.Invoke();
                break;
        }
    }

    public void UpdateHype(bool hit)
    {
        bool playerTurn = _currentBattleStage == EBattleStage.PlayerMove;
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
        OnUIUpdate?.Invoke(false, false);
    }

    public void ApplyDamage()
    {
        //Include actual damage formula later
        float damage = (currentMove.baseDamage + 10 * (currentmoveScore / currentMove.rythmData.Length));
        Debug.Log(damage + " damage dealt");
        if (_lastToMoveIsPlayer)
        {
            enemycurrenthealth -= damage;
            if (enemycurrenthealth <= 0)
            {
                BattleEnd(true);
            }
            else
            {
                correctInputCombo = 0;
                OnUIUpdate?.Invoke(true, false);
                SetBattleStage(EBattleStage.EnemyTurn);
            }
        }
        else
        {
            playercurrenthealth -= damage;
            if (playercurrenthealth <= 0)
            {
                BattleEnd(false);
            }
            else
            {
                correctInputCombo = 0;
                OnUIUpdate?.Invoke(true, true);
                SetBattleStage(EBattleStage.PlayerTurn);
            }
        }
        BattleAudioController.Instance.FadeBackToMain();
    }

    public void BattleEnd(bool winnerIsPlayer)
    {
        //do battle end stuff
        battleWinnerPlayer = winnerIsPlayer;
        SetBattleStage(EBattleStage.Conclusion);
    }

    public void RemoveEvents()
    {
        OnIntro = null;
        OnPlayerTurn = null;
        OnPlayerMove = null;
        OnEnemyTurn = null;
        OnEnemyMove = null;
        OnDamage = null;
        OnConclusion = null;
    }

    IEnumerator WaitToEnemyMove(float time = 3)
    {
        yield return new WaitForSeconds(time);
        RythmManager.Instance.PlayMove(enemyMove, false);
    }

    IEnumerator DelayedDamage(float time = 1.5f)
    {
        yield return new WaitForSeconds(time);
        ApplyDamage();
    }


}
