using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmButton : MonoBehaviour
{
    public RythmMove move;
    
    public void OnClick()
    {
        RythmManager.instance.PlayMove(move);
    }
}
