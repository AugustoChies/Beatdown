using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EquipmentType
{
    Hat,
    Goggles,
    Shirt,
    Legs,
    Arms
}

[System.Serializable]
[CreateAssetMenu()]
public class Equipment : ScriptableObject
{
    public string equipmentName;
    public string equipmentStatsBonusText;
    public int goldCost = 50;
    public float addedAtk;
    public float addedDef;
    public float addedHype;
    public float addedHP;
    public float addedRhy;
    public EquipmentType equipmentType;
    public Sprite equipmentSprite;
}
