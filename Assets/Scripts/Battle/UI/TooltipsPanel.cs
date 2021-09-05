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
        print("Resetted");
        tooltipsText.text = "";
    }

    public void ChangeTooltipText(RythmMove move)
    {
        print("Changed");
        tooltipsText.text = "\n" + move.name + "\nBase: " + move.baseDamage + "\nPerformance: " + move.performanceDamage + "\nExtra: " + move.extraDamage + "\nEffect: " + move.effect;
    }
}
