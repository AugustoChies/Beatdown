using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhaustMinigame : Minigame
{
    [SerializeField]
    private RythmMove _repeatedMove = null;
    [SerializeField]
    private AudioSource _audio = null;
    [SerializeField]
    private float _repeatAmount = 3;    

    private float currentIteration = 0;
    private float maxScore = 0;
    private float currentScore = 0;

    [SerializeField]
    private List<Sprite> backgrounds;
    [SerializeField]
    private Image backgroundFront, backgroundBack = null;
    [SerializeField]
    private float backgroundLerpTime = 1;

    private float currentTime= 0;
    private float backgroundLerp = 0;

    private RythmManager manager = null;

    private Color bufferColor = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        maxScore = _repeatedMove.rythmData.Length * (_repeatAmount + 1);
        manager = RythmManager.Instance;
        PlayerAnimator.SetTrigger(_repeatedMove.moveAnimation);
        _audio.clip = _repeatedMove.moveAudioClip;
        _audio.Play();
        manager.PlayTrainingMove(_repeatedMove);

        backgroundBack.sprite = backgrounds[Random.Range(0, backgrounds.Count)];
        do
        {
            backgroundFront.sprite = backgrounds[Random.Range(0, backgrounds.Count)];
        } while (backgroundFront.sprite == backgroundBack.sprite);
        bufferColor = backgroundFront.color;
    }

    // Update is called once per frame
    void Update()
    {
        float amount = Mathf.Lerp(1, 0, currentTime / backgroundLerpTime);
        currentTime += Time.deltaTime;
        bufferColor.a = amount;
        backgroundFront.color = bufferColor;

        if (currentTime > backgroundLerpTime)
        {
            currentTime = 0;
            backgroundFront.sprite = backgroundBack.sprite;
            do
            {
                backgroundBack.sprite = backgrounds[Random.Range(0, backgrounds.Count)];
            } while (backgroundFront.sprite == backgroundBack.sprite);
            bufferColor.a = 1.0f;
            backgroundFront.color = bufferColor;
        }        

        if (manager.MoveDone)
        {
            currentScore += manager.CurrentMoveCount;
            currentIteration++;
            if(currentIteration > _repeatAmount)
            {
                performance = currentScore / maxScore;
                ApplyGains();
            }
            else
            {
                manager.MoveDone = false;
                manager.PlayTrainingMove(_repeatedMove);
            }
        }
    }
}
