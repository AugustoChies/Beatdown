using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChampionInfoScreen : MonoBehaviour
{
    public TextMeshProUGUI charname = null;
    public TextMeshProUGUI description = null;
    public TextMeshProUGUI day = null;
    public Image portrait = null;

    void OnEnable()
    {
        BattleDataHolder dataHolder = BattleDataHolder.Instance;
        Inventory inventory = Inventory.Instance;

        charname.text = dataHolder.championInfos[inventory.ChampionVictories].Name;
        description.text = dataHolder.championInfos[inventory.ChampionVictories].Info;
        day.text = dataHolder.championInfos[inventory.ChampionVictories].DayLimit.ToString();
        portrait.sprite = dataHolder.championInfos[inventory.ChampionVictories].Portarit;
    }   
}
