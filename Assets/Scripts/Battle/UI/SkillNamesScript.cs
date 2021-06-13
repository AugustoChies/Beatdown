using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillNamesScript : UIElementScript
{
    [SerializeField]
    private TextMeshProUGUI _skillName;

    protected void Start()
    {
        BattleController.Instance.OnPlayerMove += UpdateSkillName;
        BattleController.Instance.OnEnemyMove += UpdateSkillName;
    }

    public void UpdateSkillName()
    {
        _skillName.text = BattleController.Instance.currentMove.moveName;
    }
}
