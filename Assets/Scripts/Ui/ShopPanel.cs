using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    public static ShopPanel Instance;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private Color buyableColor = Color.white;
    [SerializeField] private Color unBuyableColor = Color.red;

    public List<GameObject> ButtonsList = new List<GameObject>();

    public bool debugMode = false;
    private void Awake()
    {
        Instance = this;
    }
    
    private void OnEnable()
    {
        scrollView.normalizedPosition = new Vector2(0, 1);
        RefreshShop();
    }
    
    public void RefreshSize()
    {
        buttonsParent.GetComponent<RectTransform>().sizeDelta =
            new Vector2(0, (EquipmentManager.Instance.ListOfAllEquipments.Count - Inventory.Instance.PlayerData.ListOfObtainedEquipments.Count) 
                           * heightMultiplier);

        foreach (GameObject button in ButtonsList)
        {
            button.GetComponent<ShopButton>().UpdateMyColor(buyableColor, unBuyableColor);
        }

        goldText.text = "Gold: " + Inventory.Instance.Gold.ToString();
    }
    
    public void RefreshShop()
    {
        RefreshSize();
        
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