using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public static AnimatorTest instance;

    public Animator animator = null;
    public bool changeAnim = false;
    
    public static bool walkFw = false;
    public static bool walkRd = false;
    public static bool hip_Hop_0 = false;
    public static bool hip_Hop_1 = false;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.P) && changeAnim == false)
        {
            Debug.Log("here");
            walkFw = true;
        }
        //else if (Input.GetKey(KeyCode.P) && changeAnim == true)
        //{
        //    changeAnim = false;
        //}

        PlayAnimations();
    }

    public void PlayAnimations()
    {
        if (hip_Hop_0)
        {
            animator.SetBool("Hip_Hop_0", true);           
        }
        //else if (walkRd)
        //{
        //    animator.SetBool("Walk_Fw", false);
        //    animator.SetBool("Walk_Rd", true);
        //}
    }

    public void SetAnimation(bool var)
    {
        walkFw = var;
    }
}