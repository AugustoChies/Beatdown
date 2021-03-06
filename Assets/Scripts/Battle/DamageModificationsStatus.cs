using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class DamageModificationsStatus : ScriptableObject
{   
    public float ExtraHypeDamage = 10;
    public float PerfectionEffectMultiplier = 2f;
    public float GangGrowthMultiplier = 0.05f;
    public float SpeedEffectMultiplier = 0.2f;
    public float PerformanceBasedEffectMultiplier = 2;
    public float StartMoveDelay = 1.5f;
    public float maxHP = 500;
    public float maxAttack = 100;
    public float maxPerformance = 100;
    public float maxDefense = 100;
    public float maxRythm = 100;


    public float AtackModifier(float attackvalue)
    {
        return (1 + attackvalue/(100 + attackvalue));
    }

    public float PerformanceModifier(float performancevalue)
    {
        return (1 + performancevalue / (100 + performancevalue));
    }

    public float DefenseModifier(float defensevalue)
    {
        return (defensevalue / (100 + defensevalue));
    }

    public float RythmModifier(float rythmvalue)
    {
        return (1 + rythmvalue / (100 + rythmvalue));
    }
}
