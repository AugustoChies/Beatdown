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
    public int Gold;
    public int Victories;
    public CharacterDataClass GeneralData;

    public SaveData(int day, int hour, int gold, int victories, CharacterDataClass data)
    {
        Day = day;
        Hour = hour;
        Gold = gold;
        Victories = victories;
        GeneralData = data;
    }

    public SaveData()
    {
        Day = 0;
        Hour = 0;
        Gold = 0;
        Victories = 0;
        GeneralData = new CharacterDataClass();
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

    public GameObject waitScreen;

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

    private void Update()
    {
        if (Input.GetKeyDown(QuickSaveKey)) StartCoroutine(SaveGame());
        if (Input.GetKeyDown(QuickDeleteKey)) DeleteSaveData();
    }

    public void InitializeData()
    {
        JsonPath = Application.persistentDataPath + JsonPath;
        //print(JsonPath);
        PlayerData = new SaveData();

        LoadGame();
    }

    public void LoadGame()
    {
        string jsonString = "";
        if (File.Exists(JsonPath))
        {
            print("Save Found, Game Loaded");
            jsonString = File.ReadAllText(JsonPath);
            PlayerData = JsonUtility.FromJson<SaveData>(jsonString);
            Inventory.Instance.InitializeData(PlayerData.GeneralData, PlayerData.Hour, PlayerData.Day, PlayerData.Gold, PlayerData.Victories);
        }
        else
        {
            print("Save Not Found, Initializing Default Parameters");
            Inventory.Instance.InitializeDataDefault();
        }
    }


    public void DeleteSaveData()
    {
        print("Deleting Save");

        File.Delete(JsonPath);
    }

    public void Save()
    {
        StartCoroutine(SaveGame());
    }
    
    public IEnumerator SaveGame()
    {
        if(!isBusy)
        {
            isBusy = true;

            var inventory = Inventory.Instance;
            PlayerData = new SaveData(inventory.Day, inventory.Hour, inventory.Gold, inventory.ChampionVictories, inventory.PlayerData);

            //print(JsonPath);
            string jsonString = "";
            if (File.Exists(JsonPath))
            {
                print("Game Saved");
                jsonString = JsonUtility.ToJson(PlayerData);
                File.WriteAllText(JsonPath, jsonString);
            }
            else
            {
                print("Creating New Save Game, Please Wait");
                waitScreen.SetActive(true);

                File.Create(JsonPath);
                jsonString = JsonUtility.ToJson(PlayerData);

                yield return new WaitForSeconds(2f);

                File.WriteAllText(JsonPath, jsonString);
                
                waitScreen.SetActive(false);
                print("Game Saved");
            }

            isBusy = false;
        }
    }
}
