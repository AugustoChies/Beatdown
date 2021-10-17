using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;
    private List<Equipment> equippedItemsList;
    public float AtkBonusTotal = 0;
    public float DefBonusTotal = 0;
    public float HypeBonusTotal = 0;
    public float HPBonusTotal = 0;
    public float RhyBonusTotal = 0;
    public List<Equipment> ListOfAllEquipments;

    private void Awake()
    {
        Instance = this;
    }
    
    public void ReplaceEquipment(Equipment newEquip)
    {
        foreach (Equipment equip in Inventory.Instance.PlayerData.EquippedItems)
        {
            if (equip.equipmentType == newEquip.equipmentType)
            {
                Inventory.Instance.PlayerData.EquippedItems.Remove(equip);
                Inventory.Instance.PlayerData.EquippedItems.Add(newEquip);
                RecalculateBonusStats();
                return;
            }
        }
        
        Inventory.Instance.PlayerData.EquippedItems.Add(newEquip);
        RecalculateBonusStats();
    }

    public void UnequipItem(Equipment newEquip)
    {
        Inventory.Instance.PlayerData.EquippedItems.Remove(newEquip);
        RecalculateBonusStats();
    }

    public void RecalculateBonusStats()
    {
        AtkBonusTotal = 0;
        DefBonusTotal = 0;
        HypeBonusTotal = 0;
        HPBonusTotal = 0;
        RhyBonusTotal = 0;

        if (Inventory.Instance.PlayerData.EquippedItems.Count == 0) return;
        
        foreach (Equipment equip in Inventory.Instance.PlayerData.EquippedItems)
        {
            AtkBonusTotal += equip.addedAtk;
            DefBonusTotal += equip.addedDef;
            HypeBonusTotal += equip.addedHype;
            RhyBonusTotal += equip.addedRhy;
            HPBonusTotal += equip.addedHP;
        }
        
        if(StylesPanel.Instance) StylesPanel.Instance.RefreshStatus();
    }
}
