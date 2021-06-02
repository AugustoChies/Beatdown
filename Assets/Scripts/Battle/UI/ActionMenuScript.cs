using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuScript : UIElementScript
{
    //TODO STUFF

    //placeholder
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_mycanvas.enabled)
            {
                BattleController.Singleton.SetBattleStage(EBattleStage.PlayerMove);
            }
        }
    }
}
