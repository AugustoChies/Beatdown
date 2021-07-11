using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRythm : MonoBehaviour
{
    public static EnemyRythm Instance;
    public RythmMove rythm;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BattleController.Instance.OnEnemyTurn += PlayEnemyRythm;
    }

    public void PlayEnemyRythm()
    {
        RythmManager.Instance.PlayMove(rythm);
    }
}
