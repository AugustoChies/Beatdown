using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RythmButton : MonoBehaviour
{
    public RythmMove move;
    public Color inactiveColor = new Color32(52, 160, 200, 255);
    public Color activeColor = new Color32(236, 108, 30, 255);

    private void Start()
    {
        StartCoroutine(ResizeBoxCollider());
    }

    public void OnClick()
    {
        RythmManager.Instance.PlayMove(move, true);
    }

    public void OnMouseOver()
    {
        TooltipsPanel.Instance.ChangeTooltipText(move);
        GetComponentInChildren<Text>().color = activeColor;
    }

    public void OnMouseExit()
    {
        TooltipsPanel.Instance.ResetTooltipText();
        GetComponentInChildren<Text>().color = inactiveColor;
    }

    public IEnumerator ResizeBoxCollider()
    {
        GetComponentInChildren<Text>().color = inactiveColor;
        yield return null;
        yield return null;
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);
    }
}
