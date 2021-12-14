using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StylesPanel : MonoBehaviour
{
    public static StylesPanel Instance;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private GameObject equipmentPrefab;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private ScrollRect scrollView;
    public Color equippedColor;
    public Color unequippedColor;
    public AudioSource EquipSource = null;
    
    public List<GameObject> ButtonsList = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        scrollView.normalizedPosition = new Vector2(0, 1);
        RefreshEquipmentList();
    }

    public void RefreshStatus()
    {
        RefreshEquipmentList();

        foreach (GameObject g in ButtonsList)
        {
            g.GetComponent<EquipmentButton>().ChangeStylePanelStatus();
        }
    }

    public void RefreshEquipmentList()
    {
        buttonsParent.GetComponent<RectTransform>().sizeDelta =
            new Vector2(0, Inventory.Instance.PlayerData.ListOfObtainedEquipments.Count * heightMultiplier);

        foreach (GameObject g in ButtonsList)
        {
            Destroy(g.gameObject);
        }
        ButtonsList.Clear();

        foreach (Equipment equip in EquipmentManager.Instance.ListOfAllEquipments)
        {
            if (!Inventory.Instance.PlayerData.ListOfObtainedEquipments.Contains(equip)) continue;
            
            GameObject temp = Instantiate(equipmentPrefab, buttonsParent.transform);
            temp.GetComponent<EquipmentButton>().InitializeMyButton(equip);
        }
    }
}
