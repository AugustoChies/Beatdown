using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAudioController : MonoBehaviour
{
    public static BattleAudioController Instance;

    public AudioSource MainTrack = null;
    public AudioSource MoveTrack = null;

    private float mainVolume = 1;
    private float moveVolume = 1;

    [SerializeField]
    private float fadeTime = 0.5f;

    private void Awake()
    {
        Instance = this;
        mainVolume = MainTrack.volume;
        moveVolume = MoveTrack.volume;
    }

    public void PlayMoveTrack(AudioClip audioClip, float speed)
    {
        if (MoveTrack.isPlaying)
        {
            MoveTrack.Stop();
        }
        MoveTrack.clip = audioClip;
        MoveTrack.pitch = speed;
        MoveTrack.Play();
        StopAllCoroutines();
        StartCoroutine(FadeTracks(true));
    }

    public void FadeBackToMain()
    {
        StopAllCoroutines();
        StartCoroutine(FadeTracks(false));
    }

    IEnumerator FadeTracks(bool mainToMove)
    {
        if(mainToMove)
        {
            MainTrack.volume = mainVolume;
            MoveTrack.volume = 0;
        }
        else
        {
            MainTrack.volume = 0;
            MoveTrack.volume = moveVolume;
        }

        for (float i = 0; i < fadeTime; i+= Time.deltaTime)
        {
            if (mainToMove)
            {
                MainTrack.volume = Mathf.Lerp(1f, 0f, i / fadeTime) * mainVolume;
                MoveTrack.volume = Mathf.Lerp(0f, 1f, i / fadeTime) * moveVolume;
            }
            else
            {
                MainTrack.volume = Mathf.Lerp(0f, 1f, i / fadeTime) * mainVolume;
                MoveTrack.volume = Mathf.Lerp(1f, 0f, i / fadeTime) * moveVolume;
            }
            yield return null;
        }

        if (mainToMove)
        {
            MainTrack.volume = 0;
            MoveTrack.volume = moveVolume;
        }
        else
        {
            MainTrack.volume = mainVolume;
            MoveTrack.volume = 0;
        }
    }
}
