using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    //guto this script change animations only
    //press "P" to change animations

    public Animator animator = null;
    public bool changeAnim = false;    

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && changeAnim == false)
        {
            Debug.Log("here");
            changeAnim = true;
        }
        else if (Input.GetKey(KeyCode.P) && changeAnim == true)
        {
            changeAnim = false;
        }

        PlayAnimations();
    }

    public void PlayAnimations()
    {
        if (changeAnim)
        {
            animator.SetBool("anim_0", false);
            animator.SetBool("anim_1", true);
        }
        else
        {
            animator.SetBool("anim_0", true);
            animator.SetBool("anim_1", false);
        }
    }
}