using UnityEngine;
using UnityEngine.UI;
public class RythmMoveButtonGenerator : MonoBehaviour
{
    public GameObject moveSelectPanel;
    public GameObject rythmMoveButtonPrefab;
    public Transform firstPos;

    private RythmButton tempMove;
    [SerializeField]
    private AudioSource mySource;
    private void Start()
    {
        RythmList.Instance.Generator = this;
    }

    public void GenerateButtons()
    {
        foreach (RythmMove i in RythmList.Instance.rythmMovesList)
        {
            tempMove = Instantiate(rythmMoveButtonPrefab, firstPos.position, Quaternion.identity, moveSelectPanel.transform).GetComponent<RythmButton>();
            tempMove.move = i;
            tempMove.source = mySource;
            tempMove.gameObject.GetComponentInChildren<Text>().text = tempMove.move.moveName;
        }
    }
}
