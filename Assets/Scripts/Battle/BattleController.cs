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
    private DamageModificationsStatus statsModifiers = null;
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
    public EMoveEffect playerEffect = EMoveEffect.None;
    public EMoveEffect enemyEffect = EMoveEffect.None;
    public int currentmoveScore = 0;
    [HideInInspector]
    public RythmMove currentMove = null;

    public event Action<bool,bool> OnUIUpdate;

    public float PlayerHpPercentage => playercurrenthealth / MaxPlayerhealth * 100;
    public float enemyHpPercentage => enemycurrenthealth / MaxEnemyhealth * 100;
    public float hypeGaugePercentage => hypeBarValue * 100;

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
        CharacterData attackingChar = null;
        CharacterData defendingChar = null;
        if (_lastToMoveIsPlayer)
        {
            attackingChar = player;
            defendingChar = enemy;
        }
        else
        {
            attackingChar = enemy;
            defendingChar = player;
        }

        float damage = 0;
        float tempoMultiplier = 1;
        
        if (_lastToMoveIsPlayer)
        {
            if(playerEffect == EMoveEffect.TempoUp)
            {
                tempoMultiplier += statsModifiers.SpeedEffectMultiplier;
            }

            if (playerEffect == EMoveEffect.PerformanceBased)
            {
                damage = statsModifiers.ExtraHypeDamage * hypeBarValue;
                damage += (currentMove.performanceDamage + currentMove.baseDamage) * statsModifiers.PerformanceModifier(attackingChar.GetCurveAPerformance()) * (currentmoveScore / currentMove.rythmData.Length);
            }
            else
            {
                damage = currentMove.baseDamage * statsModifiers.AtackModifier(attackingChar.GetCurveAttack()) + statsModifiers.ExtraHypeDamage * hypeBarValue;
                damage += currentMove.performanceDamage * tempoMultiplier *statsModifiers.PerformanceModifier(attackingChar.GetCurveAPerformance()) * (currentmoveScore / currentMove.rythmData.Length);
            }

            
            if (currentmoveScore == currentMove.rythmData.Length)
            {
                if(playerEffect == EMoveEffect.Perfection)
                {
                    damage += currentMove.extraDamage * statsModifiers.PerfectionEffectMultiplier * statsModifiers.PerformanceModifier(attackingChar.GetCurveAPerformance());
                }
                else
                {
                    damage += currentMove.extraDamage * statsModifiers.PerformanceModifier(attackingChar.GetCurveAPerformance());
                }               
            }

            if (playerEffect == EMoveEffect.PerformanceBased)
            {
                damage *= statsModifiers.PerformanceBasedEffectMultiplier;
            }

            damage -= damage * statsModifiers.DefenseModifier(defendingChar.GetCurveDefense());
            enemycurrenthealth -= damage;

            OnUIUpdate?.Invoke(true, false);
            correctInputCombo = 0;            
            if (enemycurrenthealth <= 0)
            {
                BattleEnd(true);
            }
            else
            {                
                SetBattleStage(EBattleStage.EnemyTurn);
            }
        }
        else
        {
            if (enemyEffect == EMoveEffect.TempoUp)
            {
                tempoMultiplier += statsModifiers.SpeedEffectMultiplier;
            }

            if (enemyEffect == EMoveEffect.PerformanceBased)
            {
                damage = statsModifiers.ExtraHypeDamage * hypeBarValue;
                damage += (currentMove.performanceDamage + currentMove.baseDamage) * statsModifiers.PerformanceModifier(attackingChar.GetCurveAPerformance()) * (1 - (currentmoveScore / currentMove.rythmData.Length));
            }
            else
            {
                damage = currentMove.baseDamage * statsModifiers.AtackModifier(attackingChar.GetCurveAttack()) + statsModifiers.ExtraHypeDamage * hypeBarValue;
                damage += currentMove.performanceDamage * tempoMultiplier * statsModifiers.PerformanceModifier(attackingChar.GetCurveAPerformance()) * (1 - (currentmoveScore / currentMove.rythmData.Length));
            }


            if (currentmoveScore == 0)
            {
                if (enemyEffect == EMoveEffect.Perfection)
                {
                    damage += currentMove.extraDamage * statsModifiers.PerfectionEffectMultiplier * statsModifiers.PerformanceModifier(attackingChar.GetCurveAPerformance());
                }
                else
                {
                    damage += currentMove.extraDamage * statsModifiers.PerformanceModifier(attackingChar.GetCurveAPerformance());
                }

            }

            if (enemyEffect == EMoveEffect.PerformanceBased)
            {
                damage *= statsModifiers.PerformanceBasedEffectMultiplier;
            }

            damage -= damage * statsModifiers.DefenseModifier(defendingChar.GetCurveDefense());
            playercurrenthealth -= damage;

            OnUIUpdate?.Invoke(true, true);
            correctInputCombo = 0;
            if (playercurrenthealth <= 0)
            {
                BattleEnd(false);
            }
            else
            {
                SetBattleStage(EBattleStage.PlayerTurn);
            }
        }

        if (_lastToMoveIsPlayer)
        {
            playerEffect = currentMove.effect;
        }
        else
        {
            enemyEffect = currentMove.effect;
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
        RythmManager.Instance.PlayMove(EnemyAI.Instance.DecideEnemyMove(), false);
    }

    IEnumerator DelayedDamage(float time = 1.5f)
    {
        yield return new WaitForSeconds(time);
        ApplyDamage();
    }


}
