using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceAddedBehavior : MonoBehaviour
{
    [SerializeField]
    private ActiveMusicList list;

    private AudioSource mySource = null;

    private void OnValidate()
    {
        mySource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        list.AddToList(mySource);
    }

    private void OnDisable()
    {
        list.RemoveFromist(mySource);
    }
}
