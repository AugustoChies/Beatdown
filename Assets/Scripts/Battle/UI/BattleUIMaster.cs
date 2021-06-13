using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIMaster : MonoBehaviour
{
    [SerializeField]
    private ActionMenuScript _uiActionMenu = null;
    [SerializeField]
    private SkillNamesScript _uiSkillNames = null;
    [SerializeField]
    private GeneralBattleInfo _uiBattleInfo = null;

    private void Start()
    {
        BattleController.Instance.OnPlayerTurn += _uiSkillNames.Hide;
        BattleController.Instance.OnPlayerTurn += _uiActionMenu.Show;
        BattleController.Instance.OnPlayerTurn += _uiBattleInfo.Show;

        BattleController.Instance.OnPlayerMove += _uiActionMenu.Hide;
        BattleController.Instance.OnPlayerMove += _uiSkillNames.Show;

        BattleController.Instance.OnEnemyTurn += _uiSkillNames.Hide;
        BattleController.Instance.OnEnemyMove += _uiSkillNames.Show;

        BattleController.Instance.OnConclusion += _uiBattleInfo.Hide;
    }
}
