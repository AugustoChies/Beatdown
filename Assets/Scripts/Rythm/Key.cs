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
            HandleRythmKey(true, rythmKey);
        }
        if (Input.GetKeyDown(KeyCode.S) && rythmKey == RythmKey.Down && this == RythmManager.Instance.DownKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            HandleRythmKey(true, rythmKey);
        }
        if (Input.GetKeyDown(KeyCode.A) && rythmKey == RythmKey.Left && this == RythmManager.Instance.LeftKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            HandleRythmKey(true, rythmKey);
        }
        if (Input.GetKeyDown(KeyCode.D) && rythmKey == RythmKey.Right && this == RythmManager.Instance.RightKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            HandleRythmKey(true, rythmKey);
        }
    }

    public void HandleRythmKey(bool wasPressed, RythmKey side)
    {
        canPlayKeyThisFrame = false;

        switch(side)
        {
            case RythmKey.Up:
                RythmManager.Instance.UpKeysQueue.Dequeue();
                break;
            case RythmKey.Down:
                RythmManager.Instance.DownKeysQueue.Dequeue();
                break;
            case RythmKey.Left:
                RythmManager.Instance.LeftKeysQueue.Dequeue();
                break;
            case RythmKey.Right:
                RythmManager.Instance.RightKeysQueue.Dequeue();
                break;
        }

        if (canPlayKey && wasPressed)
        {
            print("Acertou");
            BattleController.Instance.UpdateHype(true);
            BattleController.Instance.currentmoveScore++;
            RythmManager.Instance.UpdateScore(score);
        }
        else
        {
            print("Errou");
            BattleController.Instance.UpdateHype(false);
        }
        RythmManager.Instance.CurrentMoveCount++;
        if (RythmManager.Instance.CurrentMoveCount == RythmManager.Instance.RythmToPlay.rythmData.Length)
        {
            BattleController.Instance.ApplyDamage();
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Center"))
        {
            canPlayKey = true;
            GetComponent<Image>().color = Color.red;
        }

        if (collision.CompareTag("InnerCenter"))
        {
            HandleRythmKey(false, rythmKey);
        }
    }
}
