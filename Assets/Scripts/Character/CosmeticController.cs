using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CosmeticController : MonoBehaviour
{
    public static CosmeticController Instance;
    
    public List<GameObject> cosmeticsList = new List<GameObject>();
    public GameObject maleHair;
    public GameObject femaleHair;

    public GameObject defaultShirtMale;
    public GameObject defaultShirtFemale;

    public int shirtID;
    public void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        print("Updating");
        
        foreach (GameObject g in cosmeticsList)
        {
            g.SetActive(false);
        }
        
            if (Inventory.Instance.IsMale)
                {
                    maleHair.SetActive(true);
                    femaleHair.SetActive(false);
                }
                else
                {
                    maleHair.SetActive(false);
                    femaleHair.SetActive(true);
                }
        
        foreach (int i in Inventory.Instance.PlayerData.EquippedItemsID)
        {
            if (i >= cosmeticsList.Count) return;
            cosmeticsList[i].SetActive(true);
            if (EquipmentManager.Instance.ListOfAllEquipments[i].hideMaleHair && maleHair.activeInHierarchy)
            {
                maleHair.SetActive(false);
            }
        }

        if (!cosmeticsList[shirtID].activeInHierarchy)
        {
            if (Inventory.Instance.IsMale)
            {
                defaultShirtMale.SetActive(true);
                defaultShirtFemale.SetActive(false);
            }
            if (!Inventory.Instance.IsMale)
            {
                defaultShirtMale.SetActive(false);
                defaultShirtFemale.SetActive(true);
            }
        }
    }
}
