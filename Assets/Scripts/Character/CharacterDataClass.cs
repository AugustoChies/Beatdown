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
    public AnimationCurve StatsCurve;
    public List<RythmMove> EquippedMoves = new List<RythmMove>();
    public List<BaseItem> Consumables;
    public List<BaseItem> Equipments;

}
