using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public CharacterData _character = null;
    public CharacterData Character => _character;
    [SerializeField]
    public ItemList _consumables = null;
    public ItemList Consumables => _consumables;
    [SerializeField]
    public ItemList _equipment = null;
    public ItemList Equipment => _equipment;
    [SerializeField]
    public int _hour = 6;
    public int Hour => _hour;
    [SerializeField]
    public int _day = 1;
    public int Day => _day;
    public Action OnUpdateTime;

    [SerializeField] private CharacterDataClass _characterData;
    public CharacterDataClass CharacterData => _characterData;

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

        _characterData.Attack = _character.Attack;
        _characterData.Health = _character.Health;
        _characterData.Defense = _character.Defense;
        _characterData.Performance = _character.Performance;
        _characterData.Rythm = _character.Rythm;
        _characterData.StatsCurve = _character.StatCurve;
        _characterData.EquippedMoves = _character.EquippedMoves;
        _characterData.Consumables = _consumables.items;
        _characterData.Equipments = _equipment.items;
    }

    public void InitializeData(CharacterDataClass characterData, int hour, int day)
    {
        _isInitialized = true;

        _characterData = characterData;
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
