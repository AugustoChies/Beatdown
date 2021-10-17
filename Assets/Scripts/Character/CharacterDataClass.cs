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
    public List<RythmMove> EquippedMoves = new List<RythmMove>();
    public List<BaseItem> Consumables;
    public List<BaseItem> Equipments;
    public List<Equipment> EquippedItems;
    public List<Equipment> ListOfObtainedEquipments;

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

}
