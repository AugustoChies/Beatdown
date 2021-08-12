using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;   

    float blendZero = 0f;
    float blendOne = 0f;
    float blendTwo = 0f;
    float blendThree = 0f;
    float blendFour = 0f;

    float blendSpeed = 1f;

    bool blendOneFinished = false;

    [SerializeField]
    private float value = 2000f;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
        blendShapeCount = skinnedMesh.blendShapeCount;
    }

    void Update()
    {
        {

            #region music

    //      if (BattleCam.endedTime)
    //      {
    //
    //          float[] spectrum;
    //
    //          spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Rectangular);
    //
    //          for (int i = 0; i < 1; i++)
    //          {
    //              skinnedMeshRenderer.SetBlendShapeWeight(0, blendZero);
    //              blendZero = spectrum[i] * value;
    //          }
    //      }
            #endregion

        }
    }
}