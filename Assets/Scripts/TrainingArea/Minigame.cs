using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatGain
{
    public float Health = 0;   
    public float Attack = 0;
    public float Performance = 0;
    public float Defense = 0;    
    public float Rythm = 0;
}


public class Minigame : MonoBehaviour
{
    [SerializeField]
    protected StatGain statGain = null;
    protected float performance = 1f;
    [HideInInspector]
    public MinigameCanvas parentCanvas = null;

    protected void ApplyGains()
    {
        Inventory.Instance.Character.ChangeStats(statGain.Health * performance, statGain.Attack * performance, statGain.Performance * performance,
            statGain.Defense * performance, statGain.Rythm * performance);
        parentCanvas.HideMinigame();
    }
}
