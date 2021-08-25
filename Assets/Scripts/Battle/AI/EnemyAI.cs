using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public static EnemyAI Instance;

    [SerializeField] private EnemyBehaviour _enemyBehaviour;
    [SerializeField] private RythmMove moveToPerform;

    [SerializeField] private List<EnemyBehaviourParameters> _enemyBehaviourParameters = new List<EnemyBehaviourParameters>();

    private void Awake()
    {
        Instance = this;
        InitializeBehaviours();
    }

    public void InitializeBehaviours()
    {
        foreach (EnemyBehaviourParameters e in _enemyBehaviour.enemyBehaviourParameters)

            _enemyBehaviourParameters.Add(e);
    }
    public RythmMove DecideEnemyMove()
    {
        updateCooldowns();

        for (int i = 0; i < _enemyBehaviourParameters.Count; i++)
        {
            if (_enemyBehaviourParameters[i].currentCooldown > 0) continue;

            if(CheckIfShouldUseMove(_enemyBehaviourParameters[i]))
            {
                if(_enemyBehaviourParameters[i].onlyUseOnce)
                {
                    _enemyBehaviourParameters.RemoveAt(i);
                }
                else
                {
                    _enemyBehaviourParameters[i].currentCooldown = _enemyBehaviourParameters[i].cooldown + 1;
                }

                moveToPerform = _enemyBehaviourParameters[i].moveToPerform;

                return moveToPerform;
            }
        }

        return moveToPerform;
    }

    public bool CheckIfShouldUseMove(EnemyBehaviourParameters parameters)
    {
        bool shouldUseThisMove = true;

        foreach (ConditionValue cond in parameters.conditions)
        {

            switch(cond.condition)
            {
                case Condition.Anytime:
                    break;
                case Condition.EnemyHpAboveXPercent:
                    if (BattleController.Instance.enemyHpPercentage < cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.EnemyHpBelowXPercent:
                    if (BattleController.Instance.enemyHpPercentage > cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.EnemyHpHigherThanPlayerHp:
                    if (BattleController.Instance.enemyHpPercentage < BattleController.Instance.PlayerHpPercentage) shouldUseThisMove = false;
                    break;
                case Condition.HypeGaugeHigherThanXPercent:
                    if (BattleController.Instance.hypeGaugePercentage < cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.HypeGaugeLowerThanXPercent:
                    if (BattleController.Instance.hypeGaugePercentage > cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.PlayerHpAboveXPercent:
                    if (BattleController.Instance.PlayerHpPercentage < cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.PlayerHpBelowXPercent:
                    if (BattleController.Instance.PlayerHpPercentage > cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.PlayerHpHigherThanEnemyHp:
                    if (BattleController.Instance.enemyHpPercentage > BattleController.Instance.PlayerHpPercentage) shouldUseThisMove = false;
                    break;
            }
        }

        return shouldUseThisMove;
    }

    public void updateCooldowns()
    {
        foreach(EnemyBehaviourParameters e in _enemyBehaviourParameters)
        {
            e.currentCooldown--;
        }
    }
}
