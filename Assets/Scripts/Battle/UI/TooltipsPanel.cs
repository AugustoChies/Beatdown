using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TooltipsPanel : MonoBehaviour
{
    public static TooltipsPanel Instance;
    public Text tooltipsTextTitle;
    public Text tooltipsTextDmg1;
    public Text tooltipsTextDmg2;
    public Text tooltipsTextDmg3;
    public Text tooltipsTextEffectDefault;
    public Text tooltipsTextEffect;

    public Color tooltipColor = new Color32(236, 108, 30, 255);

    private void Awake()
    {
        Instance = this;
        ResetTooltipText();
        //SetColor();
    }

    public void SetColor()
    {
        foreach(Transform t in GetComponentInChildren<Transform>())
        {
            t.gameObject.GetComponent<Text>().color = tooltipColor;
        }
    }

    public void ResetTooltipText()
    {
        tooltipsTextTitle.text = "";
        tooltipsTextDmg1.text = "";
        tooltipsTextDmg2.text = "";
        tooltipsTextDmg3.text = "";
        tooltipsTextEffectDefault.gameObject.SetActive(false);
        tooltipsTextEffect.text = "";
}

    public void ChangeTooltipText(RythmMove move)
    {
        tooltipsTextTitle.text = move.name;
        tooltipsTextDmg1.text = "Base: " + move.baseDamage;
        tooltipsTextDmg2.text = "Performance: " + move.performanceDamage;
        tooltipsTextDmg3.text = "Extra: " + move.extraDamage;
        tooltipsTextEffectDefault.gameObject.SetActive(true);
        tooltipsTextEffect.text = ConvertMoveEffectToString(move.effect);
    }

    public string ConvertMoveEffectToString(EMoveEffect effect)
    {
        switch(effect)
        {
            case EMoveEffect.PerformanceBased:
                return "Performance Based";
            case EMoveEffect.TempoDown:
                return "Tempo Down";
            case EMoveEffect.TempoUp:
                return "Tempo Up";
            default:
                return effect.ToString();
        }
    }
}
