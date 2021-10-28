using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDataClass 
{
    public float Health;
    public float Attack;
    public float Performance;
    public float Defense;
    public float Rythm;
    public DamageModificationsStatus modificationStatuses = null;
    public AnimationCurve StatsCurve;
    public string IdleAnimation = "";
    [HideInInspector] public List<BaseItem> Consumables; //not being used
    [HideInInspector] public List<BaseItem> Equipments; //not being used
    public List<RythmMove> EquippedMoves = new List<RythmMove>();
    public List<int> EquippedMovesID;
    public List<RythmMove> ObtainedMoves = new List<RythmMove>();
    public List<int> ObtainedMovesMovesID;
    public List<Equipment> EquippedItems;
    public List<int> EquippedItemsID;
    public List<Equipment> ListOfObtainedEquipments;
    public List<int> ListOfObtainedEquipmentsID;

    public void ChangeStats(float moreHealth, float moreAttack, float morePerformance, float moreDefense, float moreRythm)
    {
        Health += moreHealth;
        Attack += moreAttack;
        Performance += morePerformance;
        Defense += moreDefense;
        Rythm += moreRythm;

        Mathf.Clamp(Health, 0, modificationStatuses.maxHP);
        Mathf.Clamp(Attack, 0, modificationStatuses.maxAttack);
        Mathf.Clamp(Performance, 0, modificationStatuses.maxPerformance);
        Mathf.Clamp(Defense, 0, modificationStatuses.maxDefense);
        Mathf.Clamp(Rythm, 0, modificationStatuses.maxRythm);
    }

    public float GetCurveAttack()
    {
        return StatsCurve.Evaluate(Attack / 100);
    }

    public float GetCurveAPerformance()
    {
        return StatsCurve.Evaluate(Performance / 100);
    }

    public float GetCurveDefense()
    {
        return StatsCurve.Evaluate(Defense / 100);
    }

    public float GetCurveRythm()
    {
        return StatsCurve.Evaluate(Rythm / 100);
    }

}
