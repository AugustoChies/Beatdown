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
    [SerializeField]
    private ConclusionBattleCanvas _conclusionBattleInfo = null;

    private void Start()
    {        
        BattleController.Instance.OnPlayerTurn += _uiActionMenu.Show;
        BattleController.Instance.OnPlayerTurn += _uiBattleInfo.Show;

        BattleController.Instance.OnPlayerMove += _uiActionMenu.Hide;
        BattleController.Instance.OnPlayerMove += _uiSkillNames.Show;
        
        BattleController.Instance.OnEnemyMove += _uiSkillNames.Show;

        BattleController.Instance.OnDamage += _uiSkillNames.Hide;

        BattleController.Instance.OnConclusion += _uiBattleInfo.Hide;
        BattleController.Instance.OnConclusion += _uiSkillNames.Hide;
        BattleController.Instance.OnConclusion += _conclusionBattleInfo.Show;
    }
}
