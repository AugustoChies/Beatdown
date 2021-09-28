using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameCanvas : MonoBehaviour
{
    private Inventory _inventory = null;
    private GameObject _currentMinigame = null;
    public GameObject statGainPanel = null;
    private int oldHP, oldAtk, oldPerf, oldDef, oldRtm;
    [SerializeField]
    private TextMeshProUGUI _textOldHP, _textOldAttack, _textOldPerformance, _textOldDefense, _textOldRythm;
    [SerializeField]
    private TextMeshProUGUI _textNewHP, _textNewAttack, _textNewPerformance, _textNewDefense, _textNewRythm;
    private void Start()
    {
        _inventory = Inventory.Instance;
    }

    public void ShowMinigame(GameObject game)
    {
        _currentMinigame = Instantiate(game, this.transform);
        _currentMinigame.GetComponent<Minigame>().parentCanvas = this;

        oldHP = (int)_inventory.Character.Health;
        oldAtk = (int)_inventory.Character.Attack;
        oldPerf = (int)_inventory.Character.Performance;
        oldDef = (int)_inventory.Character.Defense;
        oldRtm = (int)_inventory.Character.Rythm;
    }

    public void HideMinigame()
    {
        if(_currentMinigame != null)
        {
            Destroy(_currentMinigame);
            ShowGains();
        }        
    }

    public void ShowGains()
    {
        statGainPanel.SetActive(true);

        _textOldHP.text = "" + (int)oldHP;
        _textOldAttack.text = "" + (int)oldAtk;
        _textOldPerformance.text = "" + (int)oldPerf;
        _textOldDefense.text = "" + (int)oldDef;
        _textOldRythm.text = "" + (int)oldRtm;

        _textNewHP.text = "" + (int)_inventory.Character.Health;
        _textNewAttack.text = "" + (int)_inventory.Character.Attack;
        _textNewPerformance.text = "" + (int)_inventory.Character.Performance;
        _textNewDefense.text = "" + (int)_inventory.Character.Defense;
        _textNewRythm.text = "" + (int)_inventory.Character.Rythm;
    }

    public void HideGains()
    {
        statGainPanel.SetActive(false);
    }
}
