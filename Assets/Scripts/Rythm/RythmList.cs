using System.Collections.Generic;
using UnityEngine;

public class RythmList : MonoBehaviour
{
    public static RythmList Instance;
    public RythmMoveButtonGenerator Generator;

    private void Awake()
    {
        Instance = this;
    }

    public List<RythmMove> rythmMovesList;

    public void SetPlayerMoves(List<RythmMove> moves)
    {
        rythmMovesList = moves;
        Generator.GenerateButtons();
    }
}
