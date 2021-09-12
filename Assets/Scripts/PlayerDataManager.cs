using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int Day;
    public int Hour;
    public CharacterData PlayerProxy;
    public ItemList PlayerConsumableList;
    public ItemList PlayerEquipmentList;

    public SaveData(int day, int hour, CharacterData playerProxy, ItemList playerConsumableList, ItemList playerEquipmentList)
    {
        Day = day;
        Hour = hour;
        PlayerProxy = playerProxy;
        PlayerConsumableList = playerConsumableList;
        PlayerEquipmentList = playerEquipmentList;
    }

    public SaveData()
    {

    }
}

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    public string JsonPath;
    public SaveData PlayerData;

    public KeyCode QuickSaveKey;
    public KeyCode QuickDeleteKey;

    private bool isBusy = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        InitializeData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(QuickSaveKey)) StartCoroutine(SaveGame());
        if (Input.GetKeyDown(QuickDeleteKey)) DeleteSaveData();
    }

    public void InitializeData()
    {
        JsonPath = Application.persistentDataPath + JsonPath;
        print(JsonPath);
        PlayerData = new SaveData();

        LoadGame();
    }

    public void LoadGame()
    {
        string jsonString = "";
        if (File.Exists(JsonPath))
        {
            jsonString = File.ReadAllText(JsonPath);
            PlayerData = JsonUtility.FromJson<SaveData>(jsonString);
            CloneDataToInventory();
        }
        else
        {
            print("No Save Found");
        }
    }

    public void CloneDataToInventory()
    {
        var inventory = Inventory.Instance;
        inventory._day = PlayerData.Day;
        inventory._hour = PlayerData.Hour;
        inventory._character = PlayerData.PlayerProxy;
        inventory._consumables = PlayerData.PlayerConsumableList;
        inventory._equipment = PlayerData.PlayerEquipmentList;
    }

    public void DeleteSaveData()
    {
        print("Deleting Save");

        File.Delete(JsonPath);
    }
    public IEnumerator SaveGame()
    {
        print("Saving Game");
        if(!isBusy)
        {
            isBusy = true;

            var inventory = Inventory.Instance;
            PlayerData = new SaveData(inventory.Day, inventory.Hour, inventory.Character, inventory.Consumables, inventory.Equipment);

            print(JsonPath);
            string jsonString = "";
            if (File.Exists(JsonPath))
            {
                jsonString = JsonUtility.ToJson(PlayerData);
                File.WriteAllText(JsonPath, jsonString);
            }
            else
            {
                File.Create(JsonPath);
                jsonString = JsonUtility.ToJson(PlayerData);

                yield return new WaitForSeconds(1f);

                File.WriteAllText(JsonPath, jsonString);
            }

            isBusy = false;
        }
    }
}
