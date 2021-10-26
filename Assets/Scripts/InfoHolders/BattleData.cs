using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattleData : ScriptableObject
{
    public CharacterData enemyData = null;
    public GameObject CharacterModel = null;
    public int RewardMoney = 0;
    public BaseItem RewardItem = null;
}
