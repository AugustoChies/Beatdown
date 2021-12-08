using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDataHolder : MonoBehaviour
{
    public static BattleDataHolder Instance = null;
    public BattleData CurrentBattleData = null;
    public ChampionInfo[] championInfos = null;
    public bool IsChampionBattle = false;
    public int RegularBattleDuration = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsGangBattle()
    {
        return CurrentBattleData.enemyData.IsGangMember;
    }

    public void CheckForGangID()
    {
        if (!IsGangBattle()) return;

        int id = CurrentBattleData.enemyData.GangMemberID;

        if(!Inventory.Instance.GangDefeatedIDs.Contains(id))
        {
            Inventory.Instance.GangDefeatedIDs.Add(id);
        }
    }
}
