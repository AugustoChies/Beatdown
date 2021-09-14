using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeMinigame : Minigame
{
    public float safeAreaSize = 0.5f;
    public float descentSpeed = 0.5f;
    public float inputBoost = 0.3f;
    public float timer = 10f;
    private float currentTime = 0f;
    public RectTransform bar = null;
    public RectTransform meter = null;
    public RectTransform topLimit = null;
    public RectTransform bottomLimit = null;
    

    private float limitsDistance = 0;
    public float topSafe = 0;
    public float bottomSafe = 0;
    private Vector2 descentVector;
    private Vector2 jumpVector;

    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {        
        bar.localScale = new Vector3(1,safeAreaSize,1);
        limitsDistance = topLimit.anchoredPosition.y - bottomLimit.anchoredPosition.y;
        currentTime = timer;
        descentVector = new Vector2(0, -limitsDistance * descentSpeed * Time.deltaTime);
        jumpVector = new Vector2(0, limitsDistance * inputBoost);
        topSafe = Mathf.Lerp(0, limitsDistance / 2, safeAreaSize);
        bottomSafe = topSafe * -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (done) return;

        meter.anchoredPosition += descentVector;
        currentTime -= Time.deltaTime;
        if(currentTime <= 0 || meter.anchoredPosition.y > topSafe || meter.anchoredPosition.y < bottomSafe)
        {
            Finish();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            meter.anchoredPosition += jumpVector;
        }
    }

    private void Finish()
    {
        performance = Mathf.Clamp(1 - currentTime / timer, 0, 1);
        done = true;
        ApplyGains();
    }
}
