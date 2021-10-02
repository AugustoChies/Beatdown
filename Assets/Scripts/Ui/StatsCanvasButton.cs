using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCanvasButton : MonoBehaviour
{
    [SerializeField] private GameObject myPanel;
    [SerializeField] private GameObject myButtonGlow;
    public void UpdatePanels()
    {
        StatsCanvas.Instance.ActivateThisPanel(myPanel, myButtonGlow);
    }
}
