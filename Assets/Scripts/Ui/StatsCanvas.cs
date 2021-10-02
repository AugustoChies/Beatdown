using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCanvas : MonoBehaviour
{
    public static StatsCanvas Instance;

    [SerializeField] private GameObject initialPanel;
    private GameObject currentPanel;
    [SerializeField] private GameObject initialButtonGlow;
    private GameObject currentButtonGlow;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        initialPanel.SetActive(true);
        currentPanel = initialPanel;
        initialButtonGlow.SetActive(true);
        currentButtonGlow = initialButtonGlow;
    }


    private void OnDisable()
    {
        currentPanel.SetActive(false);
        currentButtonGlow.SetActive(false);
    }

    public void ActivateThisPanel(GameObject panelToActivate, GameObject newButtonGlow)
    {
        if (currentPanel == panelToActivate) return;

        currentPanel.SetActive(false);
        panelToActivate.SetActive(true);
        currentPanel = panelToActivate;

        currentButtonGlow.SetActive(false);
        newButtonGlow.SetActive(true);
        currentButtonGlow = newButtonGlow;
    }
}
