using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class DamageModificationsStatus : ScriptableObject
{
    public float AtackModifier = 1.0f;
    public float PerformanceModifier = 1.0f;
    public float DefenseModifier = 0.5f;
    public float RythmModifier = 0.5f;
    public float ExtraHypeDamage = 10;
}
