using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private CharacterData _character = null;
    public CharacterData Character => _character;
    [SerializeField]
    private ItemList _consumables = null;
    public ItemList Consumables => _consumables;
    [SerializeField]
    private ItemList _equipment = null;
    public ItemList Equipment => _equipment;
    [SerializeField]
    private int _hour = 6;
    public int Hour => _hour;
    [SerializeField]
    private int _day = 1;
    public int Day => _day;    
    public int ChampionVictories = 0;
    public List<int> GangDefeatedIDs = null;
    [SerializeField]
    private int _gold = 100;
    public Action OnUpdateTime;
    public DamageModificationsStatus modificationsStatus = null;
    [SerializeField]
    private bool isMale;
    [SerializeField]
    private bool gameInitialized;

    public bool IsMale => isMale;
    public bool GameInitialized => gameInitialized;
    
    [SerializeField] private CharacterDataClass _playerData;
    public CharacterDataClass PlayerData => _playerData;

    public static Inventory Instance = null;

    public bool isInitialized;

    public int Gold
    {
        get => _gold;
        set => _gold = value;
    }

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!PlayerDataManager.Instance) InitializeDataDefault();
        else PlayerDataManager.Instance.InitializeData();

        /*Application.targetFrameRate = 10000;
        QualitySettings.vSyncCount = 0;*/
    }

    public bool CheckIfHasMove(RythmMove move)
    {
        return PlayerData.ObtainedMoves.Contains(move);
    }

    public void SetGender(bool chooseMale)
    {
        isMale = chooseMale;
        gameInitialized = true;
    }

    public void ResetData()
    {
        isInitialized = false;
        InitializeDataDefault();
    }

    public void InitializeDataDefault()
    {
        if (isInitialized) return;
       
        isInitialized = true;
        //print("Initializing Default");
        
        _playerData.EquippedItems.Clear();
        _playerData.EquippedMoves.Clear();
        _playerData.ListOfObtainedEquipments.Clear();
        _playerData.ObtainedMoves.Clear();
        _playerData.EquippedItemsID.Clear();
        _playerData.EquippedMovesID.Clear();
        _playerData.ListOfObtainedEquipmentsID.Clear();
        _playerData.ObtainedMovesMovesID.Clear();

        _hour = _character.defaultHour;
        _day = _character.defaultDay;
        _gold = _character.defaultGold;
        ChampionVictories = _character.defaultVictories;
        GangDefeatedIDs = new List<int>();
        
        _playerData.Attack = _character.Attack;
        _playerData.Health = _character.Health;
        _playerData.Defense = _character.Defense;
        _playerData.Performance = _character.Performance;
        _playerData.Rythm = _character.Rythm;
        _playerData.modificationStatuses = modificationsStatus;
        _playerData.StatsCurve = _character.StatCurve;
        _playerData.IdleAnimation = _character.IdleAnimation;
        _playerData.EquippedMoves = new List<RythmMove>(_character.EquippedMoves);
        _playerData.ObtainedMoves = new List<RythmMove>(_character.ObtainedMoves);
        _playerData.Consumables = new List<BaseItem>(_consumables.items);
        _playerData.Equipments = new List<BaseItem>(_equipment.items);
        _playerData.EquippedItems = new List<Equipment>(_character.equippedItems);
        _playerData.ListOfObtainedEquipments = new List<Equipment>(_character.obtainedEquippedItems);

        foreach (Equipment equip in _playerData.EquippedItems)
        {
            PlayerData.EquippedItemsID.Add(EquipmentManager.Instance.ListOfAllEquipments.IndexOf(equip));
        }
        foreach (Equipment equip in _playerData.ListOfObtainedEquipments)
        {
            PlayerData.ListOfObtainedEquipmentsID.Add(EquipmentManager.Instance.ListOfAllEquipments.IndexOf(equip));
        }
        foreach (RythmMove move in _playerData.EquippedMoves)
        {
            PlayerData.EquippedMovesID.Add(EquipmentManager.Instance.ListOfAllRythms.IndexOf(move));
        }
        foreach (RythmMove move in _playerData.ObtainedMoves)
        {
            PlayerData.ObtainedMovesMovesID.Add(EquipmentManager.Instance.ListOfAllRythms.IndexOf(move));
        }

        if(PlayerDataManager.Instance) PlayerDataManager.Instance.Save();
        EquipmentManager.Instance.RecalculateBonusStats();
    }

    public void InitializeData(CharacterDataClass characterData, int hour, int day, int gold, int bosses, List<int> GangIDs, bool male, bool initialized)
    {
        if (isInitialized) return;
        isInitialized = true;

        _playerData = characterData;
        _hour = hour;
        _day = day;
        _gold = gold;
        ChampionVictories = bosses;
        GangDefeatedIDs = GangIDs;
        isMale = male;
        gameInitialized = initialized;
        characterData.modificationStatuses = modificationsStatus;

        characterData.EquippedItems.Clear();
        characterData.EquippedMoves.Clear();
        characterData.ListOfObtainedEquipments.Clear();
        characterData.ObtainedMoves.Clear();

        foreach (int i in characterData.EquippedItemsID)
        {
            characterData.EquippedItems.Add(EquipmentManager.Instance.ListOfAllEquipments[i]);
        }
        
        foreach (int i in characterData.ListOfObtainedEquipmentsID)
        {
            characterData.ListOfObtainedEquipments.Add(EquipmentManager.Instance.ListOfAllEquipments[i]);
        }
        
        foreach (int i in characterData.EquippedMovesID)
        {
            characterData.EquippedMoves.Add(EquipmentManager.Instance.ListOfAllRythms[i]);
        }
        
        foreach (int i in characterData.ObtainedMovesMovesID)
        {
            characterData.ObtainedMoves.Add(EquipmentManager.Instance.ListOfAllRythms[i]);
        }

        EquipmentManager.Instance.RecalculateBonusStats();
        OnUpdateTime?.Invoke();
        //print("Game Loaded Successfully");
    }

    public void EndDay()
    {
        _hour = 6;
        _day++;
        OnUpdateTime?.Invoke();
    }

    public void PassTime(int hours)
    {       
        _hour += hours;
        if(_hour > 23)
        {
            _day++;
            _hour %= 24;
        }
        OnUpdateTime?.Invoke();
    }
}
