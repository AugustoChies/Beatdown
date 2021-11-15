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
    public DamageModificationsStatus maxStatsInfo = null;

    [SerializeField] private Vector3 HypeMinPos;
    [SerializeField] private Vector3 HypeMaxPos;
  //  [SerializeField] private float HypeMinValue;

    [SerializeField] private Vector3 DefMinPos;
    [SerializeField] private Vector3 DefMaxPos;
   // [SerializeField] private float DefMinValue;

    [SerializeField] private Vector3 HpMinPos;
    [SerializeField] private Vector3 HpMaxPos;
   // [SerializeField] private float HpMinValue;

    [SerializeField] private Vector3 AtkMinPos;
    [SerializeField] private Vector3 AtkMaxPos;
  //  [SerializeField] private float AtkMinValue;

    [SerializeField] private Vector3 RhyMinPos;
    [SerializeField] private Vector3 RhyMaxPos;
   // [SerializeField] private float PhyMinValue;

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
        EquipmentManager.Instance.RecalculateBonusStats();

        if (Inventory.Instance != null)
        {
            AtkCurrentValue = Inventory.Instance.PlayerData.Attack + EquipmentManager.Instance.AtkBonusTotal;
            DefCurrentValue = Inventory.Instance.PlayerData.Defense + EquipmentManager.Instance.DefBonusTotal;
            HpCurrentValue = Inventory.Instance.PlayerData.Health + EquipmentManager.Instance.HPBonusTotal;
            HypeCurrentValue = Inventory.Instance.PlayerData.Performance + EquipmentManager.Instance.HypeBonusTotal;
            RhyCurrentValue = Inventory.Instance.PlayerData.Rythm + EquipmentManager.Instance.RhyBonusTotal;

            UpdateAllStats();
        }
    }

    public void UpdateAllStats()
    {
        UpdateOneStat(Stats.Atk, AtkCurrentValue);
        AtkText.text = "ATK: " + AtkCurrentValue.ToString("F0") + "<color=green> (" + EquipmentManager.Instance.AtkBonusTotal + ")</color>";

        UpdateOneStat(Stats.Def, DefCurrentValue);
        DefText.text = "DEF: " + DefCurrentValue.ToString("F0") + "<color=green> (" + EquipmentManager.Instance.DefBonusTotal + ")</color>";

        UpdateOneStat(Stats.Rhy, RhyCurrentValue);
        RhyText.text = "RTH: " + RhyCurrentValue.ToString("F0") + "<color=green> (" + EquipmentManager.Instance.RhyBonusTotal + ")</color>";

        UpdateOneStat(Stats.Hp, HpCurrentValue);
        HpText.text = "HP: " + HpCurrentValue.ToString("F0") + "<color=green> (" + EquipmentManager.Instance.HPBonusTotal + ")</color>";

        UpdateOneStat(Stats.Hype, HypeCurrentValue);
        HypeText.text = "PRF: " + HypeCurrentValue.ToString("F0") + "<color=green> (" + EquipmentManager.Instance.HypeBonusTotal + ")</color>";
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
                return maxStatsInfo.maxAttack;
            case Stats.Def:
                return maxStatsInfo.maxDefense;
            case Stats.Hp:
                return maxStatsInfo.maxHP;
            case Stats.Hype:
                return maxStatsInfo.maxPerformance;
            case Stats.Rhy:
                return maxStatsInfo.maxRythm;
        }

        return 0;
    }

}
