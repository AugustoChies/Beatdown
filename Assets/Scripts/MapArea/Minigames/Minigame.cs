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
    [SerializeField]
    protected int hourDuration = 0;
    protected float performance = 1f;
    public MinigameCanvas parentCanvas = null;
    public Animator PlayerAnimator = null;

    protected void ApplyGains()
    {
        Inventory.Instance.PlayerData.ChangeStats(statGain.Health * performance, statGain.Attack * performance, statGain.Performance * performance,
            statGain.Defense * performance, statGain.Rythm * performance);
        Inventory.Instance.PassTime(hourDuration);
        parentCanvas.HideMinigame();
    }
}
