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
    public AudioClip moveAudioClip;
    public string moveAnimationName;
    public float baseDamage;
    public RythmData[] rythmData;
}
