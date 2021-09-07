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
