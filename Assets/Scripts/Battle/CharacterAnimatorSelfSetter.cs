using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorSelfSetter : MonoBehaviour
{
    public bool IsPlayer = false;
    public Animator anim = null;

    private void Start()
    {
        BattleController.Instance.SetAnimatorReference(IsPlayer, anim);
    }
}
