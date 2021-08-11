using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class characterCtrl : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;

    public float speed = 4f;

    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    
    public bool charMove = false;
    public bool charDance = false;


    Animator animator;

    void Start()
    {
        
    }    

    void Update()
    {
        animator = GetComponent<Animator>();

        if (
            Input.GetKey(KeyCode.W)
            )
        {
            
            charDance = true;
        }
        if (
            Input.GetKey(KeyCode.E)
            )
        {

            charDance = false;
        }
        //else
        //{
        //    charMove = false;

        //}
        setAnimations();
    }

    void setAnimations()
    {
        
        //if (charMove)
        //    animator.SetBool("charMove", true);
        //else
        //    animator.SetBool("charMove", false);
        if (charDance)
            animator.SetBool("charDance", true);
        else
            animator.SetBool("charDance", false);

    }
}
