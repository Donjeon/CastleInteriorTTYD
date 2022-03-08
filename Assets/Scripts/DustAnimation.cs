using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustAnimation : MonoBehaviour
{
    //Animation
    private Animator anim;
    private MarioMovement movementScript;

    //Checkers
    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        GameObject player = GameObject.Find("Player"); 
        movementScript = player.GetComponent<MarioMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementScript.hasLanded)
        {
            anim.SetBool("hasLanded", true);
        }

        if (!movementScript.hasLanded)
        {
            anim.SetBool("hasLanded", false);
        }

        if (movementScript.movingRight)
        {
            anim.SetBool("movingRight", true);
        }

        if (!movementScript.movingRight)
        {
            anim.SetBool("movingRight", false);
        }

    }

    public void PlayLandAnim()
    {
        anim.Play("DustLandLeft");
    }

    public void PlayDustSpinRight()
    {
        anim.Play("DustSpinRight");
    }
}
