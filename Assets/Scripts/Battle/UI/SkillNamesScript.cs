using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNamesScript : UIElementScript
{
    //TODO
    public float placeholderTime = 3;
    private float timer;

    public override void Show()
    {
        base.Show();
        timer = placeholderTime;
    }

    private void Update()
    {
        if (_mycanvas.enabled)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                BattleController.Singleton.SetBattleStage(EBattleStage.EnemyTurn);
            }
        }
    }
}
