using UnityEngine;

public class RythmButton : MonoBehaviour
{
    public RythmMove move;
    
    public void OnClick()
    {
        RythmManager.Instance.PlayMove(move, true);
    }
}
