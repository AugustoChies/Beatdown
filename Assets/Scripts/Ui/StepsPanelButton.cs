using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StepsPanelButton : MonoBehaviour
{
    [SerializeField] RythmMove myMove;
    [SerializeField] Text myNameText;
    [SerializeField] Text myStatsText;
    [SerializeField] Image myEquippedIcon;

    public void InitializeMyButton(RythmMove newMove)
    {
        myMove = newMove;
        myNameText.text = myMove.moveName;
        myStatsText.text = myMove.baseDamage + " / " + myMove.performanceDamage + " / " + myMove.extraDamage + " / " +
                           myMove.effect.ToString();

        ChangeStepsPanelStatus();
        StepsPanel.Instance.ButtonsList.Add(this.gameObject);
    }

    public void ChangeStepsPanelStatus()
    {
        if (Inventory.Instance.PlayerData.EquippedMoves.Contains(myMove))
        {
            myEquippedIcon.gameObject.SetActive(true);
            GetComponent<Image>().color = StepsPanel.Instance.equippedColor;
        }
        else
        {
            myEquippedIcon.gameObject.SetActive(false);
            GetComponent<Image>().color = StepsPanel.Instance.unequippedColor;
        }
    }
    
    public void EquipMove()
    {
        if (Inventory.Instance.PlayerData.EquippedMoves.Contains(myMove))
        {
            UnequipItem();
            return;
        }
        StepsPanel.Instance.MovesSource.Play();
        EquipmentManager.Instance.EquipMove(myMove);
    }

    public void UnequipItem()
    {
        EquipmentManager.Instance.UnequipMove(myMove);
    }
}
