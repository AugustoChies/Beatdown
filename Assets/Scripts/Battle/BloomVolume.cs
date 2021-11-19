using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomVolume : MonoBehaviour
{

    public Volume volume;
    Bloom bloom;

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
        bloom.intensity.value = Mathf.PingPong(Time.time * 2, 8);
    }
}
