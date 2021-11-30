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
    [SerializeField]
    private AudioSource minigameSource = null;
    [SerializeField]
    private AudioSource mapSource = null;

    private void Start()
    {
        _inventory = Inventory.Instance;
    }

    private AudioClip song;
    public void SetMinigameSong(AudioClip s = null)
    {
        song = s;
    }
    public void ShowMinigame(GameObject game)
    {
        _currentMinigame = Instantiate(game, this.transform);
        _currentMinigame.GetComponent<Minigame>().parentCanvas = this;

        oldHP = (int)_inventory.PlayerData.Health;
        oldAtk = (int)_inventory.PlayerData.Attack;
        oldPerf = (int)_inventory.PlayerData.Performance;
        oldDef = (int)_inventory.PlayerData.Defense;
        oldRtm = (int)_inventory.PlayerData.Rythm;

        mapSource.Stop();
        if (song != null)
        {            
            minigameSource.clip = song;
            minigameSource.Play();
        }
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

        _textNewHP.text = "" + (int)_inventory.PlayerData.Health;
        _textNewAttack.text = "" + (int)_inventory.PlayerData.Attack;
        _textNewPerformance.text = "" + (int)_inventory.PlayerData.Performance;
        _textNewDefense.text = "" + (int)_inventory.PlayerData.Defense;
        _textNewRythm.text = "" + (int)_inventory.PlayerData.Rythm;
    }

    public void HideGains()
    {
        if(minigameSource.isPlaying)
        {
            minigameSource.Stop();
        }
        mapSource.Play();
        minigameSource.clip = null;
        statGainPanel.SetActive(false);
    }
}
