using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Hat,
    Goggles,
    Shirt,
    Legs,
    Arms
}

[CreateAssetMenu()]
public class Equipment : ScriptableObject
{
    public string equipmentName;
    public string equipmentStatsBonusText;
    public float goldCost = 50;
    public float addedAtk;
    public float addedDef;
    public float addedHype;
    public float addedHP;
    public float addedRhy;
    public EquipmentType equipmentType;
    public Sprite equipmentSprite;
}
