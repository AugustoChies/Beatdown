using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TooltipsPanel : MonoBehaviour
{
    public static TooltipsPanel Instance;
    public Text tooltipsText;
 
    private void Awake()
    {
        Instance = this;
        ResetTooltipText();
    }

    public void ResetTooltipText()
    {
        tooltipsText.text = "";
    }

    public void ChangeTooltipText(RythmMove move)
    {
        tooltipsText.text = move.name + "\nBase: " + move.baseDamage + "\nPerformance: " + move.performanceDamage + "\nExtra: " + move.extraDamage + "\n:Effect: " + move.effect;
    }
}
