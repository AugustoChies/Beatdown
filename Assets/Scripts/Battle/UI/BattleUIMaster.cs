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
        BattleController.Singleton.OnPlayerTurn += _skillNames.Hide;
        BattleController.Singleton.OnPlayerTurn += _actionMenu.Show;
        BattleController.Singleton.OnPlayerMove += _actionMenu.Hide;
        BattleController.Singleton.OnPlayerMove += _skillNames.Show;
        BattleController.Singleton.OnEnemyTurn += _skillNames.Hide;
        BattleController.Singleton.OnEnemyMove += _skillNames.Show;
    }
}
