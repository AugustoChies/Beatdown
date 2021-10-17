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
    private float _performance = 10;
    public float Performance => _performance;
    [SerializeField]
    private float _defense = 10;
    public float Defense => _defense;
    [SerializeField]
    private float _rythm = 10;
    public float Rythm => _rythm;

    [SerializeField]
    private string _idleAnimation = "";
    public string IdleAnimation => _idleAnimation;

    [SerializeField]
    private AnimationCurve _statCurve = null;
    public AnimationCurve StatCurve => _statCurve;

    public List<RythmMove> EquippedMoves = null;

    public List<Equipment> equippedItems = null;
    public List<Equipment> obtainedEquippedItems = null;

    public float GetCurveAttack()
    {
        return _statCurve.Evaluate(_attack / 100);
    }

    public float GetCurveAPerformance()
    {
        return _statCurve.Evaluate(_performance / 100);
    }

    public float GetCurveDefense()
    {
        return _statCurve.Evaluate(_defense / 100);
    }

    public float GetCurveRythm()
    {
        return _statCurve.Evaluate(_rythm / 100);
    }
}
