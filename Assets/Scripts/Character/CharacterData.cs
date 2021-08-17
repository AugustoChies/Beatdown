using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private float _health = 100;
    public float Health => _health;
    [SerializeField]
    private float _attack = 10;
    public float Attack => _attack;
    [SerializeField]
    private float _defense = 10;
    public float Defense => _defense;
    [SerializeField]
    private float _rythm = 10;
    public float Rythm => _rythm;

    public List<RythmMove> EquippedMoves = null;

    public void ChangeStats(float moreHealth, float moreAttack, float moreDefense, float moreRythm)
    {
        _health += moreHealth;
        _attack += moreAttack;
        _defense += moreDefense;
        _rythm += moreRythm;
    }
}
