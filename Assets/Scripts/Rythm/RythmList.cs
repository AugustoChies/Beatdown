using System.Collections.Generic;
using UnityEngine;

public class RythmList : MonoBehaviour
{
    public static RythmList instance;
    private void Awake()
    {
        instance = this;
    }

    public List<RythmMove> rythmMovesList;
    public List<int> unlockedRythmIDs;
}
