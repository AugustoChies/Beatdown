using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RythmMove : ScriptableObject
{
   [System.Serializable]
   public struct RythmData
    {
        public float WaitTimeToNextNote;
        public float keySpeed;
        public float score;
    }

    public string moveName;
    public AudioClip moveClip;
    public RythmData[] rythmData;
}
