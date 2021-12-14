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
        if(Inventory.Instance.Gold < myEquipment.goldCost && !ShopPanel.Instance.debugMode) return;

        Inventory.Instance.Gold -= myEquipment.goldCost;
    
        Inventory.Instance.PlayerData.ListOfObtainedEquipments.Add(myEquipment);
        Inventory.Instance.PlayerData.ListOfObtainedEquipmentsID.Add(EquipmentManager.Instance.ListOfAllEquipments.IndexOf(myEquipment));

        Inventory.Instance.PlayerData.ListOfObtainedEquipments.Sort((p1,p2)=>p1.equipmentType.CompareTo(p2.equipmentType));

        ShopPanel.Instance.ButtonsList.Remove(this.gameObject);
        
        ShopPanel.Instance.RefreshSize();
        ShopPanel.Instance.BuySound.Play();
        
        Destroy(this.gameObject);
    }

    public void UpdateMyColor(Color buyableColor, Color unbuyableColor)
    {
        if (Inventory.Instance.Gold < myEquipment.goldCost) goldCostText.color = unbuyableColor;
        else goldCostText.color = buyableColor;
    }
}
