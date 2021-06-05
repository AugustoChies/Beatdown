using System.Collections;
using System.Collections.Generic;
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
        if(Input.GetKeyDown(KeyCode.W) && rythmKey == RythmKey.Up && this == RythmManager.instance.UpKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            if (canPlayKey)
            {
                RythmManager.instance.UpdateScore(score);
            }
            else
            {
                RythmManager.instance.UpdateScore(-score);
            }
            canPlayKeyThisFrame = false;
            RythmManager.instance.UpKeysQueue.Dequeue();
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S) && rythmKey == RythmKey.Down && this == RythmManager.instance.DownKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            if (canPlayKey)
            {
                RythmManager.instance.UpdateScore(score);
            }
            else
            {
                RythmManager.instance.UpdateScore(-score);
            }
            canPlayKeyThisFrame = false;
            RythmManager.instance.DownKeysQueue.Dequeue();
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.A) && rythmKey == RythmKey.Left && this == RythmManager.instance.LeftKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            if (canPlayKey)
            {
                RythmManager.instance.UpdateScore(score);
            }
            else
            {
                RythmManager.instance.UpdateScore(-score);
            }
            canPlayKeyThisFrame = false;
            RythmManager.instance.LeftKeysQueue.Dequeue();
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D) && rythmKey == RythmKey.Right && this == RythmManager.instance.RightKeysQueue.Peek() && canPlayKeyThisFrame)
        {
            if (canPlayKey)
            {
                RythmManager.instance.UpdateScore(score);
            }
            else
            {
                RythmManager.instance.UpdateScore(-score);
            }
            canPlayKeyThisFrame = false;
            RythmManager.instance.RightKeysQueue.Dequeue();
            Destroy(this.gameObject);
        }
    }

    private void LateUpdate()
    {
        canPlayKeyThisFrame = true;
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
