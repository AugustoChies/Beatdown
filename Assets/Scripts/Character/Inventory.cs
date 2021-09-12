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

    public static Inventory Instance = null;

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
