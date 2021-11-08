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
    private Image _playerStatus;
    [SerializeField]
    private Image _enemyStatus;
    [SerializeField]
    private TextMeshProUGUI _comboText;
    [SerializeField]
    private Sprite[] statusIcons;


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

    private void UpdateGUI(bool updateHealth,bool isPlayer = true)//Check for which health bar to update
    {
        _hypeBar.fillAmount = BattleController.Instance.hypeBarValue;
        int combo = BattleController.Instance.correctInputCombo;

        if (combo > 0)
        {
            _comboText.enabled = true;
            _comboText.text = "Combo " + combo;   
        }
        else
        {
            _comboText.enabled = false;
        }

        if (!updateHealth) return;

        int statusplayer = (int)BattleController.Instance.playerEffect;
        int statusenemy = (int)BattleController.Instance.enemyEffect;
        
        if(statusplayer == 0)
        {
            _playerStatus.enabled = false;
        }
        else
        {
            _playerStatus.enabled = true;
            _playerStatus.sprite = statusIcons[statusplayer - 1];
        }

        if (statusenemy == 0)
        {
            _enemyStatus.enabled = false;
        }
        else
        {
            _enemyStatus.enabled = true;
            _enemyStatus.sprite = statusIcons[statusenemy - 1];
        }

        if (isPlayer)
        {           
            StartCoroutine(HealthScroll(0.5f, BattleController.Instance.playercurrenthealth / BattleController.Instance.MaxPlayerhealth, _playerHealth));
        }
        else
        {
            StartCoroutine(HealthScroll(0.5f, BattleController.Instance.enemycurrenthealth / BattleController.Instance.MaxEnemyhealth, _enemyHealth));
        }
    }

    IEnumerator HealthScroll(float scrolltime, float newfillAmount, Image imageToFill)
    {
        float oldFill = imageToFill.fillAmount;
        for (float i = 0; i < scrolltime; i+= Time.deltaTime)
        {
            imageToFill.fillAmount = Mathf.Lerp(oldFill, newfillAmount, i/scrolltime);
            yield return null;
        }
        imageToFill.fillAmount = newfillAmount;
    }
}
