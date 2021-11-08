using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GangMemberPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _moneyReward;
    [SerializeField]
    private TextMeshProUGUI _itemReward;
    [SerializeField]
    private Image _portrait;
    [SerializeField]
    private Image _rank;

    [SerializeField]
    private List<Sprite> rankSprites;

    [SerializeField]
    private GangMemberInfo _infoObject;

    private void Awake()
    {
        _name.text = _infoObject.Name;
        _moneyReward.text = string.Concat("$" + _infoObject.BattleInfo.RewardMoney);
        if (_infoObject.BattleInfo.RewardMove != null)
        {
            _itemReward.text = string.Concat("Reward: " ,_infoObject.BattleInfo.RewardMove.moveName);
        }
        else
        {
            _itemReward.text ="Reward: None";
        }

        _portrait.sprite = _infoObject.Portarit;
        _rank.sprite = rankSprites[_infoObject.StrenghtLevel];
        //Rank Puxa uma imagem especifica de uma lista baseando o índice no numero do StrenghtLevel do infoobject - 1;

    }
}
