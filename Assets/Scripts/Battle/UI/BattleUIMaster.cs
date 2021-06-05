using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIMaster : MonoBehaviour
{
    [SerializeField]
    private ActionMenuScript _actionMenu = null;
    [SerializeField]
    private SkillNamesScript _skillNames = null;

    private void Start()
    {
        BattleController.Instance.OnPlayerTurn += _skillNames.Hide;
        BattleController.Instance.OnPlayerTurn += _actionMenu.Show;

        BattleController.Instance.OnPlayerMove += _actionMenu.Hide;
        BattleController.Instance.OnPlayerMove += _skillNames.Show;

        BattleController.Instance.OnEnemyTurn += _skillNames.Hide;
        BattleController.Instance.OnEnemyMove += _skillNames.Show;
    }
}
