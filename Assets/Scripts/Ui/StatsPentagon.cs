using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public enum Stats
{
    Hype, 
    Def,
    Hp,
    Atk,
    Phy
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

    [SerializeField] private Vector3 PhyMinPos;
    [SerializeField] private Vector3 PhyMaxPos;
   // [SerializeField] private float PhyMinValue;
    [SerializeField] private float PhyMaxValue;


    [SerializeField] private Stats statToDebug;
    [SerializeField] private float StatToDebugValue;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            UpdateOneStat(statToDebug);
        }
    }

    public void UpdateOneStat(Stats _statToDebug)
    {
        if(shapeController == null ) shapeController = GetComponent<SpriteShapeController>();

        float percentage = (StatToDebugValue / MaxStatValue(_statToDebug));

        Vector3 newPointPos = GetStatMinPosition(_statToDebug) - ((GetStatMinPosition(_statToDebug) - GetStatMaxPosition(_statToDebug)) * percentage);

        shapeController.spline.SetPosition((int)statToDebug, newPointPos);
    }

    public void UpdateOneStat(Stats _statToDebug, float percentage)
    {
        if (shapeController == null) shapeController = GetComponent<SpriteShapeController>();

        shapeController.spline.SetPosition((int)statToDebug, new Vector3(10, 10, 10));
    }

    //public float StatRange(Stats _statToDebug)
    //{
    //    switch(_statToDebug)
    //    {
    //        case Stats.Atk:
    //            return AtkMaxValue - AtkMinValue;
    //        case Stats.Def:
    //            return DefMaxValue - DefMinValue;
    //        case Stats.Hp:
    //            return HpMaxValue - HpMinValue;
    //        case Stats.Hype:
    //            return HypeMaxValue - HypeMinValue;
    //        case Stats.Phy:
    //            return PhyMaxValue - PhyMinValue;
    //    }

    //    return 0;
    //}

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
            case Stats.Phy:
                return PhyMaxPos;
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
            case Stats.Phy:
                return PhyMinPos;
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
            case Stats.Phy:
                return PhyMaxValue;
        }

        return 0;
    }

    //public float MinStatValue(Stats _statToDebug)
    //{
    //    switch (_statToDebug)
    //    {
    //        case Stats.Atk:
    //            return AtkMinValue;
    //        case Stats.Def:
    //            return DefMinValue;
    //        case Stats.Hp:
    //            return HpMinValue;
    //        case Stats.Hype:
    //            return HypeMinValue;
    //        case Stats.Phy:
    //            return PhyMinValue;
    //    }

    //    return 0;
    //}
}
