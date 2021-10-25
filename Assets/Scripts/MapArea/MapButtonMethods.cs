using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MapButtonMethods : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _dateText = null;

    private void Start()
    {
        UpdateTime();
        Inventory.Instance.OnUpdateTime += UpdateTime;
    }

    private void OnDestroy()
    {
        Inventory.Instance.OnUpdateTime -= UpdateTime;
    }

    public void StartBattleButton()
    {
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
    }
}
