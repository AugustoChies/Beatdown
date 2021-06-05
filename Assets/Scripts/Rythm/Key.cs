using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public RythmKey rythmKey;
    public float speed;
    public float score;

    public Transform centerPoint;

    public bool canPlayKey;

    public static bool canPlayKeyThisFrame = true;

    private void Update()
    {
        if(centerPoint) transform.position = Vector2.MoveTowards(transform.position, centerPoint.transform.position, speed * Time.deltaTime);

        HandleKey();
    }

    public void HandleKey()
    {
        if(Input.GetKeyDown(KeyCode.W) && rythmKey == RythmKey.Up && this == RythmManager.Instance.UpKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            if (canPlayKey)
            {
                RythmManager.Instance.UpdateScore(score);
            }
            else
            {
                RythmManager.Instance.UpdateScore(-score);
            }
            canPlayKeyThisFrame = false;
            RythmManager.Instance.UpKeysQueue.Dequeue();
            RythmManager.Instance.CurrentMoveCount++;
            if(RythmManager.Instance.CurrentMoveCount == RythmManager.Instance.RythmToPlay.rythmData.Length)
            {
                BattleController.Instance.SetBattleStage(EBattleStage.EnemyTurn);
            }
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S) && rythmKey == RythmKey.Down && this == RythmManager.Instance.DownKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            if (canPlayKey)
            {
                RythmManager.Instance.UpdateScore(score);
            }
            else
            {
                RythmManager.Instance.UpdateScore(-score);
            }
            canPlayKeyThisFrame = false;
            RythmManager.Instance.DownKeysQueue.Dequeue();
            RythmManager.Instance.CurrentMoveCount++;
            if (RythmManager.Instance.CurrentMoveCount == RythmManager.Instance.RythmToPlay.rythmData.Length)
            {
                BattleController.Instance.SetBattleStage(EBattleStage.EnemyTurn);
            }
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.A) && rythmKey == RythmKey.Left && this == RythmManager.Instance.LeftKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            if (canPlayKey)
            {
                RythmManager.Instance.UpdateScore(score);
            }
            else
            {
                RythmManager.Instance.UpdateScore(-score);
            }
            canPlayKeyThisFrame = false;
            RythmManager.Instance.LeftKeysQueue.Dequeue();
            RythmManager.Instance.CurrentMoveCount++;
            if (RythmManager.Instance.CurrentMoveCount == RythmManager.Instance.RythmToPlay.rythmData.Length)
            {
                BattleController.Instance.SetBattleStage(EBattleStage.EnemyTurn);
            }
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D) && rythmKey == RythmKey.Right && this == RythmManager.Instance.RightKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            if (canPlayKey)
            {
                RythmManager.Instance.UpdateScore(score);
            }
            else
            {
                RythmManager.Instance.UpdateScore(-score);
            }
            canPlayKeyThisFrame = false;
            RythmManager.Instance.RightKeysQueue.Dequeue();
            RythmManager.Instance.CurrentMoveCount++;
            if (RythmManager.Instance.CurrentMoveCount == RythmManager.Instance.RythmToPlay.rythmData.Length)
            {
                BattleController.Instance.SetBattleStage(EBattleStage.EnemyTurn);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Center"))
        {
            canPlayKey = true;
            GetComponent<Image>().color = Color.red;
        }
    }
}
