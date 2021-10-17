using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopButton : MonoBehaviour
{
    [SerializeField] Equipment myEquipment;
    [SerializeField] Text myNameText;
    [SerializeField] Text myStatsText;
    [SerializeField] Text goldCostText;
    [SerializeField] Image icon;

    public void InitializeMyButton(Equipment newEquipment)
    {
        myEquipment = newEquipment;
        myNameText.text = myEquipment.equipmentName;
        myStatsText.text = myEquipment.equipmentStatsBonusText;
        goldCostText.text = myEquipment.goldCost.ToString("F0") + "G";
        icon.sprite = myEquipment.equipmentSprite;
        
        ShopPanel.Instance.ButtonsList.Add(this.gameObject);
    }

    public void UnlockItem()
    {
        Inventory.Instance.PlayerData.ListOfObtainedEquipments.Add(myEquipment);
        
        Inventory.Instance.PlayerData.ListOfObtainedEquipments.Sort((p1,p2)=>p1.equipmentType.CompareTo(p2.equipmentType));

        ShopPanel.Instance.RefreshSize();
        
        Destroy(this.gameObject);
    }
}
