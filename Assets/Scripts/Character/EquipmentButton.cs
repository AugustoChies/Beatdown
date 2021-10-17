using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentButton : MonoBehaviour
{
    [SerializeField] Equipment myEquipment;
    [SerializeField] Text myNameText;
    [SerializeField] Text myStatsText;
    [SerializeField] Image myIcon;
    [SerializeField] Image myEquippedIcon;

    public void InitializeMyButton(Equipment newEquipment)
    {
        myEquipment = newEquipment;
        myNameText.text = myEquipment.equipmentName;
        myStatsText.text = myEquipment.equipmentStatsBonusText;
        myIcon.sprite = myEquipment.equipmentSprite;

       ChangeStylePanelStatus();
       StylesPanel.Instance.ButtonsList.Add(this.gameObject);
    }

    public void ChangeStylePanelStatus()
    {
        if (Inventory.Instance.PlayerData.EquippedItems.Contains(myEquipment))
        {
            myEquippedIcon.gameObject.SetActive(true);
            GetComponent<Image>().color = StylesPanel.Instance.equippedColor;
        }
        else
        {
            myEquippedIcon.gameObject.SetActive(false);
            GetComponent<Image>().color = StylesPanel.Instance.unequippedColor;
        }
    }
    
    public void EquipItem()
    {
        if (Inventory.Instance.PlayerData.EquippedItems.Contains(myEquipment))
        {
            UnequipItem();
            return;
        }
        EquipmentManager.Instance.ReplaceEquipment(myEquipment);
    }

    public void UnequipItem()
    {
        EquipmentManager.Instance.UnequipItem(myEquipment);

    }
}
