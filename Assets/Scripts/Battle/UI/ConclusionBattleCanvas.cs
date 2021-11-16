using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ConclusionBattleCanvas : UIElementScript
{
    //Later use this script/object to display all battle conclusion info. Just a placeholder.
    [SerializeField]
    private GameObject victoryObject = null;
    [SerializeField]
    private GameObject defeatObject = null;
    [SerializeField]
    private string afterBattleScene = "";

    [SerializeField]
    private TextMeshProUGUI _moneyReward = null;
    [SerializeField]
    private TextMeshProUGUI _moveReward = null;


    public override void Show()
    {
        base.Show();
        
        victoryObject.SetActive(BattleController.Instance.battleWinnerPlayer);
        defeatObject.SetActive(!BattleController.Instance.battleWinnerPlayer);

        if(BattleController.Instance.battleWinnerPlayer)
        {
            Inventory inventory = Inventory.Instance;
            _moneyReward.text = BattleDataHolder.Instance.CurrentBattleData.RewardMoney.ToString();
            inventory.Gold += BattleDataHolder.Instance.CurrentBattleData.RewardMoney;
            if (BattleDataHolder.Instance.CurrentBattleData.RewardMove == null)
            {
                _moveReward.text = "Nothing";
            }
            else
            {                
                RythmMove newMove = BattleDataHolder.Instance.CurrentBattleData.RewardMove;
                if (!inventory.PlayerData.ObtainedMoves.Contains(newMove))
                {
                    inventory.PlayerData.ObtainedMovesMovesID.Add(EquipmentManager.Instance.ListOfAllRythms.IndexOf(newMove));
                    inventory.PlayerData.ObtainedMoves.Add(newMove);
                }
                _moveReward.text = newMove.moveName;
            }

            if(BattleDataHolder.Instance.IsChampionBattle)
            {
                inventory.ChampionVictories += 1;
            }
        }
    }

    public void DoneButton()
    {
        if (BattleDataHolder.Instance.IsChampionBattle)
        {
            Inventory.Instance.EndDay();
        }
        else
        {
            Inventory.Instance.PassTime(BattleDataHolder.Instance.RegularBattleDuration);
        }
        
        SceneManager.LoadScene(afterBattleScene);
    }
}
