using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public RythmKey rythmKey;
    public float speed;
    public float score;

    public Transform centerPoint;

    public bool canPlayKey;

    public Sprite activeSprite; 

    public static bool canPlayKeyThisFrame = true;

    private void Update()
    {
        if(centerPoint) transform.position = Vector2.MoveTowards(transform.position, centerPoint.transform.position, speed * Time.deltaTime);

        HandleKey();
    }

    public void HandleKey()
    {
        if (RythmManager.Instance.KeysQueue.Count == 0) return;
        if (this != RythmManager.Instance.KeysQueue.Peek()) return;
        if (!canPlayKeyThisFrame) return;
        
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            HandleRythmKey(true, RythmKey.Up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            HandleRythmKey(true, RythmKey.Down);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            HandleRythmKey(true, RythmKey.Left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HandleRythmKey(true, RythmKey.Right);
        }
    }

    public void HandleRythmKey(bool wasPressed, RythmKey side)
    {
        if (RythmManager.Instance.KeysQueue.Count == 0) return;

        canPlayKeyThisFrame = false;

        RythmKey rightSide = RythmManager.Instance.KeysQueue.Peek().rythmKey;
        
        if (canPlayKey && wasPressed && (rightSide == side))
        {
            //print("Acertou");
            BattleController.Instance.UpdateHype(true);
            BattleController.Instance.currentmoveScore++;
        }
        else
        {
            //print("Errou");
            BattleController.Instance.UpdateHype(false);
        }
        
        switch(side)
        {
            case RythmKey.Up:
                RythmManager.Instance.KeysQueue.Dequeue();
                break;
            case RythmKey.Down:
                RythmManager.Instance.KeysQueue.Dequeue();
                break;
            case RythmKey.Left:
                RythmManager.Instance.KeysQueue.Dequeue();
                break;
            case RythmKey.Right:
                RythmManager.Instance.KeysQueue.Dequeue();
                break;
        }

        
        RythmManager.Instance.CurrentMoveCount++;
        if (RythmManager.Instance.CurrentMoveCount == RythmManager.Instance.RythmToPlay.rythmData.Length)
        {
            BattleController.Instance.SetBattleStage(EBattleStage.DamageStep);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Center") && RythmManager.Instance.KeysQueue.Peek() == this && canPlayKey == false)
        {
            GetComponent<Image>().sprite = activeSprite;
            canPlayKey = true;
            //GetComponent<Image>().color = Color.red;
        }

        if (collision.CompareTag("InnerCenter"))
        {
            HandleRythmKey(false, rythmKey);
        }
    }
}
