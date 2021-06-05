using UnityEngine;

public class KeyInner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Center"))
        {
            RythmManager.Instance.UpdateScore(-GetComponentInParent<Key>().score);
            RythmManager.Instance.CurrentMoveCount++;
            if (RythmManager.Instance.CurrentMoveCount == RythmManager.Instance.RythmToPlay.rythmData.Length)
            {
                BattleController.Instance.SetBattleStage(EBattleStage.EnemyTurn);
            }
            Destroy(transform.parent.gameObject);
        }
    }
}
