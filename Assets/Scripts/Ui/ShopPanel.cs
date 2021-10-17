using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopPanel : MonoBehaviour
{
    public static ShopPanel Instance;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private ScrollRect scrollView;

    public List<GameObject> ButtonsList = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }

    public void RefreshSize()
    {
        buttonsParent.GetComponent<RectTransform>().sizeDelta =
            new Vector2(0, (EquipmentManager.Instance.ListOfAllEquipments.Count - Inventory.Instance.PlayerData.ListOfObtainedEquipments.Count) 
                           * heightMultiplier);
    }

    private void OnEnable()
    {
        RefreshShop();
    }
    public void RefreshShop()
    {
        RefreshSize();
        
        scrollView.normalizedPosition = new Vector2(0, 1);

        foreach (GameObject g in ButtonsList)
        {
            Destroy(g.gameObject);
        }
        ButtonsList.Clear();

        foreach (Equipment equip in EquipmentManager.Instance.ListOfAllEquipments)
        {
            if (Inventory.Instance.PlayerData.ListOfObtainedEquipments.Contains(equip)) continue;
            
            GameObject temp = Instantiate(shopItemPrefab, buttonsParent.transform);
            temp.GetComponent<ShopButton>().InitializeMyButton(equip);
        }
    }
}