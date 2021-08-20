using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConditionValue
{
    public Condition condition;
    public float threshold;
}

[CreateAssetMenu()]
public class EnemyBehaviour : ScriptableObject
{
    public List<EnemyBehaviourParameters> enemyBehaviourParameters;
}

[Serializable]
public class EnemyBehaviourParameters
{
    public RythmMove moveToPerform;
    public int cooldown;
    [HideInInspector] public int currentCooldown;
    public bool onlyUseOnce;
    public List<ConditionValue> conditions;
}