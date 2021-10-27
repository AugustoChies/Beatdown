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
    private GangMemberInfo _infoObject;
}
