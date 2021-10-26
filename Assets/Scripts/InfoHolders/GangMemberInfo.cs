using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GangMemberInfo : ScriptableObject
{
    public string Name = "";
    public Sprite Portarit = null;
    public int StrenghtLevel = 1;
    public BattleData BattleInfo = null;
}
