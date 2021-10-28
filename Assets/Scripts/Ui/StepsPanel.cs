using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StepsPanel : MonoBehaviour
{
    public static StepsPanel Instance;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private GameObject movePrefab;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private ScrollRect scrollView;
    public Color equippedColor;
    public Color unequippedColor;
    
    public List<GameObject> ButtonsList = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        RefreshMovesList();
    }

    public void RefreshStatus()
    {
        foreach (GameObject g in ButtonsList)
        {
            g.GetComponent<StepsPanelButton>().ChangeStepsPanelStatus();
        }
    }

    public void RefreshMovesList()
    {
        buttonsParent.GetComponent<RectTransform>().sizeDelta =
            new Vector2(0, Inventory.Instance.PlayerData.ListOfObtainedEquipments.Count * heightMultiplier);
        
        scrollView.normalizedPosition = new Vector2(0, 1);

        foreach (GameObject g in ButtonsList)
        {
            Destroy(g.gameObject);
        }
        ButtonsList.Clear();

        foreach (RythmMove move in EquipmentManager.Instance.ListOfAllRythms)
        {
            if (!Inventory.Instance.PlayerData.ObtainedMoves.Contains(move)) continue;
            
            GameObject temp = Instantiate(movePrefab, buttonsParent.transform);
            temp.GetComponent<StepsPanelButton>().InitializeMyButton(move);
        }
    }
}
