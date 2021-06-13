using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralBattleInfo : UIElementScript
{
    private bool _initialized = false;

    [SerializeField]
    private Image _hypeBar;
    [SerializeField]
    private Image _playerHealth;
    [SerializeField]
    private Image _enemyHealth;
    [SerializeField]
    private TextMeshProUGUI _comboText;

    protected void Start()
    {
        BattleController.Instance.OnUIUpdate += UpdateGUI;
    }

    public override void Show()
    {
        if (!_initialized)
        {
            _initialized = true;
            _comboText.enabled = false;
            base.Show();
        }
    }

    private void UpdateGUI()
    {
        _hypeBar.fillAmount = BattleController.Instance.hypeBarValue;
        int combo = BattleController.Instance.correctInputCombo;

        if (combo > 0)
        {
            _comboText.enabled = true;
            _comboText.text = "Combo: " + combo;   
        }
        else
        {
            _comboText.enabled = false;
        }

        _playerHealth.fillAmount = BattleController.Instance.playercurrenthealth / 100;
        _playerHealth.fillAmount = BattleController.Instance.enemycurrenthealth / 100;
    }
}
