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

    private void Update()
    {
        if(centerPoint) transform.position = Vector2.MoveTowards(transform.position, centerPoint.transform.position, speed * Time.deltaTime);

        HandleKey();
    }

    public void HandleKey()
    {
        if(Input.GetKeyDown(KeyCode.W) && rythmKey == RythmKey.Up)
        {
            if (canPlayKey)
            {
                RythmManager.instance.UpdateScore(score);
            }
            else
            {
                RythmManager.instance.UpdateScore(-score);
            }
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S) && rythmKey == RythmKey.Down)
        {
            if (canPlayKey)
            {
                RythmManager.instance.UpdateScore(score);
            }
            else
            {
                RythmManager.instance.UpdateScore(-score);
            }
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.A) && rythmKey == RythmKey.Left)
        {
            if (canPlayKey)
            {
                RythmManager.instance.UpdateScore(score);
            }
            else
            {
                RythmManager.instance.UpdateScore(-score);
            }
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D) && rythmKey == RythmKey.Right)
        {
            if (canPlayKey)
            {
                RythmManager.instance.UpdateScore(score);
            }
            else
            {
                RythmManager.instance.UpdateScore(-score);
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
