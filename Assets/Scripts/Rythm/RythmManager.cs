using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmManager : MonoBehaviour
{
    public static RythmManager Instance;

    [SerializeField]
    private DamageModificationsStatus _modifiers;
    public float score;
    private float _speedModifier = 1.0f;

    public RythmMove RythmToPlay;

    public Text scoreText;
    public GameObject RythmKeyPrefab;
    public GameObject CenterPoint;
    public GameObject UpPoint;
    public GameObject DownPoint;
    public GameObject RightPoint;
    public GameObject LeftPoint;
    public GameObject RythmPanel;

    public Queue<Key> KeysQueue = new Queue<Key>();

    public int CurrentMoveCount = 0;
    
    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        UpdateScore(0);
    }

    private void LateUpdate()
    {
        Key.canPlayKeyThisFrame = true;
        //Debug.Log("Upq " +  UpKeysQueue.Count);
    }

    public void UpdateScore(float value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void PlayMove(RythmMove move, bool isPlayer)
    {
        RythmToPlay = move;
        CurrentMoveCount = 0;
        _speedModifier = 1.0f;
        

        if (isPlayer)
        {
            BattleController.Instance.SetBattleStage(EBattleStage.PlayerMove, move);
        }
        else
        {
            BattleController.Instance.SetBattleStage(EBattleStage.EnemyMove, move);
        }

        StartCoroutine(PlayRythm(isPlayer, move, _modifiers.StartMoveDelay));
    }

    public void CreateKey(int index)
    {
        int side = Random.Range(0, 4);

        Key temp = null;

        switch(side)
        {
            case 0:
                temp = Instantiate(RythmKeyPrefab, UpPoint.transform.position, Quaternion.Euler(0, 0, 180), RythmPanel.transform).GetComponent<Key>();
                temp.rythmKey = RythmKey.Up;
                KeysQueue.Enqueue(temp);
                break;
            case 1:
                temp = Instantiate(RythmKeyPrefab, DownPoint.transform.position, Quaternion.Euler(0, 0, 0), RythmPanel.transform).GetComponent<Key>();
                temp.rythmKey = RythmKey.Down;
                KeysQueue.Enqueue(temp);
                break;
            case 2:
                temp = Instantiate(RythmKeyPrefab, LeftPoint.transform.position, Quaternion.Euler(0, 0, -90), RythmPanel.transform).GetComponent<Key>();
                temp.rythmKey = RythmKey.Left;
                KeysQueue.Enqueue(temp);
                break;
            case 3:
                temp = Instantiate(RythmKeyPrefab, RightPoint.transform.position, Quaternion.Euler(0, 0, 90), RythmPanel.transform).GetComponent<Key>();
                temp.rythmKey = RythmKey.Right;
                KeysQueue.Enqueue(temp);
                break;
            default:
                print("Error");
                break;
        }

        if(temp)
        {
            temp.speed = RythmToPlay.rythmData[index].keySpeed * _speedModifier;
            temp.score = RythmToPlay.rythmData[index].score;
            temp.centerPoint = CenterPoint.transform;
        }
    }

    public IEnumerator PlayRythm(bool isPlayer, RythmMove move, float waittime)
    {
        yield return new WaitForSeconds(waittime);

        if (isPlayer)
        {
            BattleController.Instance.SetPlayerAnimation(move.moveAnimation);

            if (BattleController.Instance.playerEffect == EMoveEffect.TempoUp)
            {
                _speedModifier = 1.2f;
            }
            if (BattleController.Instance.playerEffect == EMoveEffect.TempoDown)
            {
                _speedModifier = 0.8f;
               
            }
        }
        else
        {
            BattleController.Instance.SetEnemyAnimation(move.moveAnimation);

            if (BattleController.Instance.enemyEffect == EMoveEffect.TempoUp)
            {
                _speedModifier = 1.2f;
            }
            if (BattleController.Instance.enemyEffect == EMoveEffect.TempoDown)
            {
                _speedModifier = 0.8f;
            }
        }
        BattleAudioController.Instance.PlayMoveTrack(move.moveAudioClip, _speedModifier);
        BattleController.Instance.currentmoveScore = 0; //reset current move score

        int currentIndex = 0;

        while(currentIndex < RythmToPlay.rythmData.Length)
        {
            CreateKey(currentIndex);
            yield return new WaitForSeconds(RythmToPlay.rythmData[currentIndex].WaitTimeToNextNote * (2 - _speedModifier));
            currentIndex++;
        }        

        yield return null;
    }
}
