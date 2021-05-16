using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Center"))
        {
            RythmManager.instance.UpdateScore(-GetComponentInParent<Key>().score);
            Destroy(transform.parent.gameObject);
        }
    }
}
