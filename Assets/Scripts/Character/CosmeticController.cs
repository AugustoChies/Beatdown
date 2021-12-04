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
        
        foreach (int i in Inventory.Instance.PlayerData.EquippedItemsID)
        {
            if (i >= cosmeticsList.Count) return;
            cosmeticsList[i].SetActive(true);
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
    }
}
