using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorSelfSetter : MonoBehaviour
{
    public bool IsPlayer = false;
    private Animator anim = null;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Start()
    {
        BattleController.Instance.SetAnimatorReference(IsPlayer, anim);
    }
}
