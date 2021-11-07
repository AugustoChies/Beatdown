using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticController : MonoBehaviour
{
    public List<GameObject> cosmeticsList = new List<GameObject>();
    private void Start()
    {
        foreach (int i in Inventory.Instance.PlayerData.ListOfObtainedEquipmentsID)
        {
            if (i >= cosmeticsList.Count) return;
            cosmeticsList[i].SetActive(true);
        }
    }
}
