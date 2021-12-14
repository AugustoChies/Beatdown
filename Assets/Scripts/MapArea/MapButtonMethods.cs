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
    [SerializeField]
    private AudioSource buttonSource = null;
    [SerializeField]
    private AudioClip buttonSound = null;
    [SerializeField]
    private AudioClip championBattleStart = null;


    private void Start()
    {
        UpdateTime();
        Inventory.Instance.OnUpdateTime += UpdateTime;
    }

    private void OnDestroy()
    {
        Inventory.Instance.OnUpdateTime -= UpdateTime;
    }
    
    public void ButtonSound()
    {
        buttonSource.clip = buttonSound;
        buttonSource.Play();
    }

    public void StartBattleButton(BattleData battleData)
    {
        ButtonSound();
        BattleDataHolder.Instance.CurrentBattleData = battleData;
        BattleDataHolder.Instance.IsChampionBattle = false;
        if(BattleDataHolder.Instance.CurrentBattleData.dialogue != null)
        {
            DialogueManager.Instance.PlayDialogue(BattleDataHolder.Instance.CurrentBattleData.dialogue);
        }
        else
        {
            SceneManager.LoadSceneAsync("BattleScene",LoadSceneMode.Single);
        }
    }

    public void StartChampionBattleButton()
    {
        buttonSource.clip = championBattleStart;
        buttonSource.Play();
        BattleDataHolder.Instance.CurrentBattleData = BattleDataHolder.Instance.championInfos[Inventory.Instance.ChampionVictories].BattleInfo;
        BattleDataHolder.Instance.IsChampionBattle = true;
        DialogueManager.Instance.PlayDialogue(BattleDataHolder.Instance.CurrentBattleData.dialogue);
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
        Inventory.Instance.isInitialized = false;
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
