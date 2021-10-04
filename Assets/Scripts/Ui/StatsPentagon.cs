using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;

public enum Stats
{
    Hype, 
    Def,
    Hp,
    Atk,
    Rhy
}

public class StatsPentagon : MonoBehaviour
{
    private SpriteShapeController shapeController;

    [SerializeField] private Vector3 HypeMinPos;
    [SerializeField] private Vector3 HypeMaxPos;
  //  [SerializeField] private float HypeMinValue;
    [SerializeField] private float HypeMaxValue;

    [SerializeField] private Vector3 DefMinPos;
    [SerializeField] private Vector3 DefMaxPos;
   // [SerializeField] private float DefMinValue;
    [SerializeField] private float DefMaxValue;

    [SerializeField] private Vector3 HpMinPos;
    [SerializeField] private Vector3 HpMaxPos;
   // [SerializeField] private float HpMinValue;
    [SerializeField] private float HpMaxValue;

    [SerializeField] private Vector3 AtkMinPos;
    [SerializeField] private Vector3 AtkMaxPos;
  //  [SerializeField] private float AtkMinValue;
    [SerializeField] private float AtkMaxValue;

    [SerializeField] private Vector3 RhyMinPos;
    [SerializeField] private Vector3 RhyMaxPos;
   // [SerializeField] private float PhyMinValue;
    [SerializeField] private float RhyMaxValue;

    [SerializeField] private float AtkCurrentValue;
    [SerializeField] private float DefCurrentValue;
    [SerializeField] private float HpCurrentValue;
    [SerializeField] private float HypeCurrentValue;
    [SerializeField] private float RhyCurrentValue;

    [SerializeField] private TextMeshProUGUI RhyText;
    [SerializeField] private TextMeshProUGUI AtkText;
    [SerializeField] private TextMeshProUGUI DefText;
    [SerializeField] private TextMeshProUGUI HpText;
    [SerializeField] private TextMeshProUGUI HypeText;


    private void OnEnable()
    {
        if (Inventory.Instance != null)
        {
            AtkCurrentValue = Inventory.Instance.PlayerData.Attack;
            DefCurrentValue = Inventory.Instance.PlayerData.Defense;
            HpCurrentValue = Inventory.Instance.PlayerData.Health;
            HypeCurrentValue = Inventory.Instance.PlayerData.Performance;
            RhyCurrentValue = Inventory.Instance.PlayerData.Rythm;

            UpdateAllStats();
        }
    }

    public void UpdateAllStats()
    {
        UpdateOneStat(Stats.Atk, AtkCurrentValue);
        AtkText.text = AtkCurrentValue.ToString("F0");

        UpdateOneStat(Stats.Def, DefCurrentValue);
        DefText.text = DefCurrentValue.ToString("F0");

        UpdateOneStat(Stats.Rhy, RhyCurrentValue);
        RhyText.text = RhyCurrentValue.ToString("F0");

        UpdateOneStat(Stats.Hp, HpCurrentValue);
        HpText.text = HpCurrentValue.ToString("F0");

        UpdateOneStat(Stats.Hype, HypeCurrentValue);
        HypeText.text = HypeCurrentValue.ToString("F0");
    }

    public void UpdateOneStat(Stats _statToDebug, float StatToDebugValue)
    {
        if(shapeController == null ) shapeController = GetComponent<SpriteShapeController>();

        float percentage = (StatToDebugValue / MaxStatValue(_statToDebug));

        Vector3 newPointPos = GetStatMinPosition(_statToDebug) - ((GetStatMinPosition(_statToDebug) - GetStatMaxPosition(_statToDebug)) * percentage);

        shapeController.spline.SetPosition((int)_statToDebug, newPointPos);
    }

    public Vector3 GetStatMaxPosition(Stats _statToDebug)
    {
        switch (_statToDebug)
        {
            case Stats.Atk:
                return AtkMaxPos;
            case Stats.Def:
                return DefMaxPos;
            case Stats.Hp:
                return HpMaxPos;
            case Stats.Hype:
                return HypeMaxPos;
            case Stats.Rhy:
                return RhyMaxPos;
        }

        return Vector3.zero;
    }

    public Vector3 GetStatMinPosition(Stats _statToDebug)
    {
        switch (_statToDebug)
        {
            case Stats.Atk:
                return AtkMinPos;
            case Stats.Def:
                return DefMinPos;
            case Stats.Hp:
                return HpMinPos;
            case Stats.Hype:
                return HypeMinPos;
            case Stats.Rhy:
                return RhyMinPos;
        }

        return Vector3.zero;
    }

    public float MaxStatValue(Stats _statToDebug)
    {
        switch (_statToDebug)
        {
            case Stats.Atk:
                return AtkMaxValue;
            case Stats.Def:
                return DefMaxValue;
            case Stats.Hp:
                return HpMaxValue;
            case Stats.Hype:
                return HypeMaxValue;
            case Stats.Rhy:
                return RhyMaxValue;
        }

        return 0;
    }

}
