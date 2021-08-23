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
    [SerializeField]
    private float extraHypeDamage = 10;
    public float hypeBarValue = 0;
    public int correctInputCombo = 0;

    public CharacterData player = null;
    public CharacterData enemy = null;
    public float playercurrenthealth = 100;
    public float enemycurrenthealth = 100;
    [HideInInspector]
    public float MaxPlayerhealth = 100;
    [HideInInspector]
    public float MaxEnemyhealth = 100;
    /////////////////   
    private bool _lastToMoveIsPlayer = true;
    public bool battleWinnerPlayer = false;
    public int currentmoveScore = 0;
    [HideInInspector]
    public RythmMove currentMove = null;

    public event Action<bool,bool> OnUIUpdate;

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);

        Instance = this;
    }

    private void SetCharacterValues()
    {
        playercurrenthealth = player.Health;
        enemycurrenthealth = enemy.Health;
        MaxPlayerhealth = player.Health;
        MaxEnemyhealth = enemy.Health;
        RythmList.Instance.SetPlayerMoves(player.EquippedMoves);
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
                SetCharacterValues();
                pauseController.CanPause = false;
                OnIntro?.Invoke();
                break;
            case EBattleStage.PlayerTurn:
                pauseController.CanPause = true;                
                OnPlayerTurn?.Invoke();
                break;
            case EBattleStage.PlayerMove:
                _lastToMoveIsPlayer = true;
                OnPlayerMove?.Invoke();
                break;
            case EBattleStage.EnemyTurn:
                pauseController.CanPause = true;                
                OnEnemyTurn?.Invoke();
                StartCoroutine(WaitToEnemyMove());
                break;
            case EBattleStage.EnemyMove:
                _lastToMoveIsPlayer = false;                
                OnEnemyMove?.Invoke();
                break;
            case EBattleStage.DamageStep:
                OnDamage?.Invoke();
                StartCoroutine(DelayedDamage());
                break;
            case EBattleStage.Conclusion:
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
        float damage = currentMove.baseDamage + extraHypeDamage * hypeBarValue;
        
        if (_lastToMoveIsPlayer)
        {
            damage += currentMove.performanceDamage * (currentmoveScore / currentMove.rythmData.Length);
            if (currentmoveScore == currentMove.rythmData.Length)
            {
                damage += currentMove.extraDamage;
            }
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
            damage += currentMove.performanceDamage *  (1 - (currentmoveScore / currentMove.rythmData.Length));
            if (currentmoveScore == 0)
            {
                damage += currentMove.extraDamage;
            }
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
        Debug.Log(damage + " damage dealt");
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
        int randomMove = UnityEngine.Random.Range(0, enemy.EquippedMoves.Count);
        RythmManager.Instance.PlayMove(enemy.EquippedMoves[randomMove], false);
    }

    IEnumerator DelayedDamage(float time = 1.5f)
    {
        yield return new WaitForSeconds(time);
        ApplyDamage();
    }


}
