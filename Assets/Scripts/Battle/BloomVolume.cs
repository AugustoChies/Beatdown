using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomVolume : MonoBehaviour
{

    public Volume volume;

    public static BloomVolume instance;

    Bloom bloom;
    public static bool pullingUp = false;

    void Start()
    {
        if (volume.profile.TryGet<Bloom>(out bloom))
        {
            //bloom.intensity.value;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pullingUp)
        {
            print("UP");
            bloom.intensity.value = Mathf.PingPong(Time.time * 2, 8);
        }
        else
        {
            bloom.intensity.value = Mathf.PingPong(Time.time * 2, -8);
        }
    }
}
