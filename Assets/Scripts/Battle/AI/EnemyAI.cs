using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyBehaviour _enemyBehaviour;
    public RythmMove moveToPerform;

    private List<EnemyBehaviourParameters> _enemyBehaviourParameters = new List<EnemyBehaviourParameters>();

    private void Awake()
    {
        InitializeBehaviours();
    }

    public void InitializeBehaviours()
    {
        foreach (EnemyBehaviourParameters e in _enemyBehaviour.enemyBehaviourParameters)

            _enemyBehaviourParameters.Add(e);
    }
    public RythmMove DecideEnemyMove()
    {
        for(int i = 0; i < _enemyBehaviourParameters.Count; i++)
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
                    _enemyBehaviourParameters[i].currentCooldown = _enemyBehaviourParameters[i].cooldown;
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
                    if (BattleController.Instance.enemycurrenthealth < cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.EnemyHpBelowXPercent:
                    if (BattleController.Instance.enemycurrenthealth > cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.EnemyHpHigherThanPlayerHp:
                    if (BattleController.Instance.enemycurrenthealth < BattleController.Instance.playercurrenthealth) shouldUseThisMove = false;
                    break;
                case Condition.HypeGaugeHigherThanXPercent:
                    if (BattleController.Instance.hypeBarValue < cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.HypeGaugeLowerThanXPercent:
                    if (BattleController.Instance.hypeBarValue > cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.PlayerHpAboveXPercent:
                    if (BattleController.Instance.playercurrenthealth < cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.PlayerHpBelowXPercent:
                    if (BattleController.Instance.playercurrenthealth > cond.threshold) shouldUseThisMove = false;
                    break;
                case Condition.PlayerHpHigherThanEnemyHp:
                    if (BattleController.Instance.enemycurrenthealth > BattleController.Instance.playercurrenthealth) shouldUseThisMove = false;
                    break;
            }
        }

        return shouldUseThisMove;
    }
}
