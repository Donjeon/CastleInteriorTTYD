using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioSound : MonoBehaviour
{

    AudioSource marioAudioSource;


    //Dust
    public GameObject dustTrailRight;
    public GameObject dustTrailLeft;
    public GameObject dustTrailBackward;
    public GameObject dustTrailForward;


    public GameObject player;

    public MarioMovement moveScript;

    

    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        marioAudioSource = GetComponent <AudioSource>();
        //set playerPositon variable to the position of the player
        GameObject player = GameObject.Find("Player"); //does this do anything?

        moveScript = player.GetComponent<MarioMovement>();
    }

    private void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //Debug.Log(playerPosition);

        
    }

    private void MarioFootstepSound()
    {
        marioAudioSource.Play();
    }

    private void CreateDustTrailRight()
    {
        if (moveScript.movingRight && moveScript.isGrounded)
        {
            GameObject dustTrail = Instantiate(dustTrailRight, playerPosition, transform.rotation);
        }
        
    }

    private void CreateDustTrailLeft()
    {
        if (moveScript.movingLeft && moveScript.isGrounded)
        {
            GameObject dustTrail = Instantiate(dustTrailLeft, playerPosition, transform.rotation);
        }

    }

    private void CreateDustTrailBackward()
    {
        if (moveScript.movingBackward && !moveScript.movingRight && !moveScript.movingLeft && moveScript.isGrounded)
        {
            GameObject dustTrail = Instantiate(dustTrailBackward, playerPosition, transform.rotation);
        }

    }

    private void CreateDustTrailForward()
    {
        if (moveScript.movingForward && !moveScript.movingRight && !moveScript.movingLeft && moveScript.isGrounded)
        {
            GameObject dustTrail = Instantiate(dustTrailForward, playerPosition, transform.rotation);
        }

    }
}
