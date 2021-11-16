using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MapButtonMethods : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _dateText = null;
    [SerializeField]
    private GameObject _gameover = null;

    private void Start()
    {
        UpdateTime();
        Inventory.Instance.OnUpdateTime += UpdateTime;
    }

    private void OnDestroy()
    {
        Inventory.Instance.OnUpdateTime -= UpdateTime;
    }

    public void StartBattleButton(BattleData battleData)
    {
        BattleDataHolder.Instance.CurrentBattleData = battleData;
        BattleDataHolder.Instance.IsChampionBattle = false;
        SceneManager.LoadScene("BattleScene");
    }

    public void StartChampionBattleButton()
    {
        BattleDataHolder.Instance.CurrentBattleData = BattleDataHolder.Instance.championInfos[Inventory.Instance.ChampionVictories].BattleInfo;
        BattleDataHolder.Instance.IsChampionBattle = true;
        SceneManager.LoadScene("BattleScene");
    }

    public void UpdateTime()
    {
        int day = Inventory.Instance.Day;
        int hour = Inventory.Instance.Hour;

        string ampm = hour < 12 ? "am" : "pm";

        if(hour > 11)
        {
            hour -= 12;
        }

        _dateText.text = "Day " + day + "  " + hour + " " + ampm;

        GameOverCheck();
    }

    public void GameOverReload()
    {        
        PlayerDataManager.Instance.LoadGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void GameOverCheck()
    {
        if(Inventory.Instance.Day > BattleDataHolder.Instance.championInfos[Inventory.Instance.ChampionVictories].DayLimit)
        {
            _gameover.SetActive(true);
        }
    }
}
