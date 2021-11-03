using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    private BattleDataHolder _battledata;
    private BattleController _battlecontroller;

    private void Awake()
    {
        _battledata = BattleDataHolder.Instance;
        _battlecontroller = BattleController.Instance;

        Instantiate(_battledata.CurrentBattleData.CharacterModel, this.transform.position, this.transform.rotation, this.transform);
        _battlecontroller.enemy = _battledata.CurrentBattleData.enemyData;
    }
}
