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
    [SerializeField]
    private int _gold = 100;
    public int Gold => _gold;
    public Action OnUpdateTime;
    public DamageModificationsStatus modificationsStatus = null;

    [SerializeField] private CharacterDataClass _playerData;
    public CharacterDataClass PlayerData => _playerData;

    public static Inventory Instance = null;

    private bool _isInitialized;
    
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
    }

    public void InitializeDataDefault()
    {
        if (_isInitialized) return;
       
        _isInitialized = true;
        //print("Initializing Default");

        _playerData.Attack = _character.Attack;
        _playerData.Health = _character.Health;
        _playerData.Defense = _character.Defense;
        _playerData.Performance = _character.Performance;
        _playerData.Rythm = _character.Rythm;
        _playerData.modificationStatuses = modificationsStatus;
        _playerData.StatsCurve = _character.StatCurve;
        _playerData.IdleAnimation = _character.IdleAnimation;
        _playerData.EquippedMoves = _character.EquippedMoves;
        _playerData.Consumables = _consumables.items;
        _playerData.Equipments = _equipment.items;
        _playerData.EquippedItems = _character.equippedItems;
        _playerData.ListOfObtainedEquipments = _character.obtainedEquippedItems;

        foreach (Equipment equip in _playerData.EquippedItems)
        {
            PlayerData.EquippedItemsID.Add(_playerData.EquippedItems.IndexOf(equip));
        }
        foreach (Equipment equip in _playerData.ListOfObtainedEquipments)
        {
            PlayerData.ListOfObtainedEquipmentsID.Add(_playerData.ListOfObtainedEquipments.IndexOf(equip));
        }
        foreach (RythmMove move in _playerData.EquippedMoves)
        {
            PlayerData.EquippedMovesID.Add(_playerData.EquippedMoves.IndexOf(move));
        }

        if(PlayerDataManager.Instance) PlayerDataManager.Instance.Save();
        EquipmentManager.Instance.RecalculateBonusStats();
    }

    public void InitializeData(CharacterDataClass characterData, int hour, int day, int gold)
    {
        _isInitialized = true;

        _playerData = characterData;
        _hour = hour;
        _day = day;
        _gold = gold;
        characterData.modificationStatuses = modificationsStatus;

        characterData.EquippedItems.Clear();
        characterData.EquippedMoves.Clear();
        characterData.ListOfObtainedEquipments.Clear();

        foreach (int i in characterData.EquippedItemsID)
        {
            characterData.EquippedItems.Add(EquipmentManager.Instance.ListOfAllEquipments[i]);
        }
        
        foreach (int i in characterData.EquippedMovesID)
        {
            characterData.EquippedMoves.Add(EquipmentManager.Instance.ListOfAllRythms[i]);
        }
        
        foreach (int i in characterData.ListOfObtainedEquipmentsID)
        {
            characterData.ListOfObtainedEquipments.Add(EquipmentManager.Instance.ListOfAllEquipments[i]);
        }
        
        EquipmentManager.Instance.RecalculateBonusStats();
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
