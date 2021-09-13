using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAnimation : MonoBehaviour
{
    //Animation
    private Animator anim;
    //public SpriteRenderer marioSR;


    //Get other variables
    private MarioMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");

        movementScript = player.GetComponent<MarioMovement>();
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (movementScript.isMoving & movementScript.isGrounded) //if you arent moving AND you arent grounded, then play the idle animation
        {
            
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (movementScript.isGrounded)
        {
            anim.SetBool("isAirborne", false);
        } else
        {
            anim.SetBool("isAirborne", true);
        }

        /*
        if (!marioSR.flipX && movementScript.movingRight)
        {
            marioSR.flipX = true;
        } else if(marioSR.flipX && movementScript.movingLeft) 
        {
            marioSR.flipX = false;
        }
        */

        //left and right animation
        if(movementScript.movingLeft)
        {
            anim.SetBool("movingLeft", true);
            anim.SetBool("movingRight", false);
        }

        if(movementScript.movingRight)
        {
            anim.SetBool("movingRight", true);
            anim.SetBool("movingLeft", false);
        }

        if(!movementScript.movingRight && !movementScript.movingLeft)
        {
            anim.SetBool("movingRight", false);
            anim.SetBool("movingLeft", false);
        }
        
        //forward and backward animation
        if(movementScript.movingForward)
        {
            anim.SetBool("movingForward", true);
            anim.SetBool("movingBackward", false);
        }

        if(movementScript.movingBackward)
        {
            anim.SetBool("movingForward", false);
            anim.SetBool("movingBackward", true);
        }

        if(!movementScript.movingForward && !movementScript.movingBackward)
        {
            anim.SetBool("movingForward", false);
            anim.SetBool("movingBackward", false);
        }
    }
}
