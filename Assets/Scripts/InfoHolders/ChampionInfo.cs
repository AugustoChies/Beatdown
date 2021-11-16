using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChampionInfo : ScriptableObject
{
    public string Name = "";
    public string Info = "";
    public Sprite Portarit = null;
    public int DayLimit = 1;
    public BattleData BattleInfo = null;
}
