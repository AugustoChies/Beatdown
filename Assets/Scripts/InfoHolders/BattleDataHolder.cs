using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDataHolder : MonoBehaviour
{
    public static BattleDataHolder Instance = null;
    public BattleData CurrentBattleData = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
