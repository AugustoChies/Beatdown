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
    public Action OnUpdateTime;

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
        _playerData.StatsCurve = _character.StatCurve;
        _playerData.EquippedMoves = _character.EquippedMoves;
        _playerData.Consumables = _consumables.items;
        _playerData.Equipments = _equipment.items;
    }

    public void InitializeData(CharacterDataClass characterData, int hour, int day)
    {
        _isInitialized = true;

        _playerData = characterData;
        _hour = hour;
        _day = day;
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
        if(hours > 23)
        {
            _day++;
            _hour %= 24;
        }
        OnUpdateTime?.Invoke();
    }
}
