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
    public List<RythmMove> ListOfAllRythms;
    
    [Header("Debug")]
    public bool unequipLastMoveAutomatically = false;
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
                
                Inventory.Instance.PlayerData.EquippedItemsID.Remove(ListOfAllEquipments.IndexOf(equip));
                Inventory.Instance.PlayerData.EquippedItemsID.Add(ListOfAllEquipments.IndexOf(newEquip));

                RecalculateBonusStats();
                return;
            }
        }
        
        Inventory.Instance.PlayerData.EquippedItems.Add(newEquip);
        Inventory.Instance.PlayerData.EquippedItemsID.Add(ListOfAllEquipments.IndexOf(newEquip));

        RecalculateBonusStats();
    }

    public void UnequipItem(Equipment newEquip)
    {
        Inventory.Instance.PlayerData.EquippedItems.Remove(newEquip);
        Inventory.Instance.PlayerData.EquippedItemsID.Remove(ListOfAllEquipments.IndexOf(newEquip));

        RecalculateBonusStats();
    }

    public void EquipMove(RythmMove newMove)
    {
        if (Inventory.Instance.PlayerData.EquippedMoves.Count >= 3)
        {
            if(!unequipLastMoveAutomatically) return;
            
            Inventory.Instance.PlayerData.EquippedMoves.RemoveAt(0);
            Inventory.Instance.PlayerData.EquippedMovesID.RemoveAt(0);
        }
        
        Inventory.Instance.PlayerData.EquippedMoves.Add(newMove);
        Inventory.Instance.PlayerData.EquippedMovesID.Add(ListOfAllRythms.IndexOf(newMove));
        
        StepsPanel.Instance.RefreshStatus();
    }

    public void UnequipMove(RythmMove newMove)
    {
        Inventory.Instance.PlayerData.EquippedMoves.Remove(newMove);
        Inventory.Instance.PlayerData.EquippedMovesID.Remove(ListOfAllRythms.IndexOf(newMove));
        StepsPanel.Instance.RefreshStatus();
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
