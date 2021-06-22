using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveMusciList")]
public class ActiveMusicList : ScriptableObject
{
    public List<AudioSource> activesources = new List<AudioSource>();

    public void PauseAll()
    {
        for (int i = 0; i < activesources.Count; i++)
        {
            activesources[i].Pause();
        }
    }

    public void ResumeAll()
    {
        for (int i = 0; i < activesources.Count; i++)
        {
            activesources[i].UnPause();
        }
    }

    public void AddToList(AudioSource source)
    {
        activesources.Add(source);
    }

    public void RemoveFromist(AudioSource source)
    {
        activesources.Remove(source);
    }
}
