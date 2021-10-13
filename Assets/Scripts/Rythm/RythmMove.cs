using UnityEngine;

public enum EMoveEffect
{
    None,
    TempoUp,
    TempoDown,
    Perfection,
    PerformanceBased
}

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
    public string moveAnimation;
    public float baseDamage;
    public float performanceDamage;
    public float extraDamage;
    public EMoveEffect effect;
    public RythmData[] rythmData;
}
