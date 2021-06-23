using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ConclusionBattleCanvas : UIElementScript
{
    //Later use this script/object to display all battle conclusion info. Just a placeholder.
    [SerializeField]
    private GameObject victoryObject = null;
    [SerializeField]
    private GameObject defeatObject = null;
    [SerializeField]
    private string afterBattleScene = "";
   

    public override void Show()
    {
        base.Show();

        victoryObject.SetActive(BattleController.Instance.battleWinnerPlayer);
        defeatObject.SetActive(!BattleController.Instance.battleWinnerPlayer);
    }

    public void DoneButton()
    {
        SceneManager.LoadScene(afterBattleScene);
    }
}
