using UnityEngine;
using UnityEngine.UI;
public class RythmMoveButtonGenerator : MonoBehaviour
{
    public GameObject moveSelectPanel;
    public GameObject rythmMoveButtonPrefab;
    public Transform firstPos;

    private RythmButton tempMove;
    private void Start()
    {
        foreach(int i in RythmList.instance.unlockedRythmIDs)
        {
            tempMove = Instantiate(rythmMoveButtonPrefab, firstPos.position, Quaternion.identity, moveSelectPanel.transform).GetComponent<RythmButton>();
            tempMove.move = RythmList.instance.rythmMovesList[i];
            tempMove.gameObject.GetComponentInChildren<Text>().text = tempMove.move.moveName;
        }
    }
}
