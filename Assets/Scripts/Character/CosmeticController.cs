using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticController : MonoBehaviour
{
    public List<GameObject> cosmeticsList = new List<GameObject>();
    public GameObject maleHair;
    public GameObject femaleHair;

    private void Start()
    {
        foreach (int i in Inventory.Instance.PlayerData.ListOfObtainedEquipmentsID)
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
