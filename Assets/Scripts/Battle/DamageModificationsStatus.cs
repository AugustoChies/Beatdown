using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class DamageModificationsStatus : ScriptableObject
{   
    public float ExtraHypeDamage = 10;
    public float PerfectionEffectMultiplier = 2f;
    public float SpeedEffectMultiplier = 0.2f;
    public float PerformanceBasedEffectMultiplier = 2;

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
