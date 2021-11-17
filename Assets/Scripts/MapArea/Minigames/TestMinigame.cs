using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMinigame : Minigame
{
    public Image filled;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(FillBar());
    }

    IEnumerator FillBar()
    {        
        filled.fillAmount = 0f;
        for (float i = 0; i < 2; i+= Time.deltaTime)
        {
            filled.fillAmount = i / 2;
            yield return null;
        }
        filled.fillAmount = 1f;
        ApplyGains();
    }
}
