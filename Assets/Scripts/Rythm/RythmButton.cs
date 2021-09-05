using UnityEngine;
using System.Collections;
public class RythmButton : MonoBehaviour
{
    public RythmMove move;

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
    }

    public void OnMouseExit()
    {
        TooltipsPanel.Instance.ResetTooltipText();
    }

    public IEnumerator ResizeBoxCollider()
    {
        yield return null;
        yield return null;
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);
    }
}
