using UnityEngine;

public class RythmButton : MonoBehaviour
{
    public RythmMove move;
    public bool isMouseOverThis = false;

    public void OnClick()
    {
        RythmManager.Instance.PlayMove(move, true);
    }

    private void OnMouseOver()
    {
        if(!isMouseOverThis)
        {
            isMouseOverThis = true;
            TooltipsPanel.Instance.ChangeTooltipText(move);
        }
    }

    private void OnMouseExit()
    {
        if(isMouseOverThis)
        {
            isMouseOverThis = false;
            TooltipsPanel.Instance.ResetTooltipText();
        }
    }
}
