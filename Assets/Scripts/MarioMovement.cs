using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))] //prevents bugs

public class MarioMovement : MonoBehaviour
{
    //sound
    public AudioClip marioJump1, marioJump2;
    public AudioClip marioLand;

    //Movement
    [Tooltip("Mario's movement speed when on the ground and force is applied via movement keys")]
    public float moveSpeed;
    [Tooltip("A negative float. The closer the value to 0, the greater the gravity")]
    public float gravity = -9.8f;
    public float jumpForce;
    [Tooltip("The value that movement input is multiplied by when Mario is airborne. Default: 100")]
    public float airborneMoveSpeed = 100f;
    [Tooltip("The maximum speed Mario can move at. If he goes over this limit, his speed is readjusted to 0.01 lower in that direction")]
    public float moveSpeedCap = 5.01f;

    private Vector2 moveInput;
    public float groundDistance;
    public LayerMask groundMask;
    public Transform groundPoint;


    //Checker variables
    Vector3 velocity;
    public bool isGrounded;
    public bool isMoving;
    private int frameCount = 1;
    
    public bool movingLeft;
    public bool movingRight;
    public bool movingBackward;
    public bool movingForward;

    private bool wasAirborne = false;

    //Player 
    public GameObject player;
    public Rigidbody playerRB;



   

    void Update()
    {
        //frame counter
        frameCount++;
        if(frameCount == 51)
        {
            frameCount = 1;
            
        }

        //groundCheck
        if (Physics.CheckSphere(groundPoint.position, groundDistance, groundMask)) //check if the player is touching the ground:
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        //Movement on ground
        if (isGrounded)
        {
            playerRB.velocity = new Vector3(moveInput.x * moveSpeed, playerRB.velocity.y, moveInput.y * moveSpeed);

        }
        else //Movement in air
        {
            float actualAirborneMoveSpeed = airborneMoveSpeed / 10000000;

            if(frameCount == 10) //to limit how quickly this force is applied, its applied only every 10 frames
            {
                if (movingBackward)
                {
                    playerRB.AddRelativeForce(0, 0, -actualAirborneMoveSpeed); //x y z forcemode
                }

                if (movingForward)
                {
                    playerRB.AddRelativeForce(0, 0, actualAirborneMoveSpeed);
                }

                if (movingLeft)
                {
                    playerRB.AddRelativeForce(actualAirborneMoveSpeed, 0, 0);
                }

                if (movingRight)
                {
                    playerRB.AddRelativeForce(-actualAirborneMoveSpeed, 0, 0);
                }
            }
        }

        //Speed limiters
        if (playerRB.velocity.x > moveSpeedCap && playerRB.velocity.x > 0) //if going left past the moveSpeedCap and velocity is greater than 0 (so no conflicts when is negative i.e going right), set the move speed on X to moveSpeedCap -0.01
        {
            Vector3 v = playerRB.velocity;
            v.x = moveSpeedCap - 0.01f;
            playerRB.velocity = v;
        }

        if (playerRB.velocity.x < -moveSpeedCap && playerRB.velocity.x < 0) //right
        {
            Vector3 v = playerRB.velocity;
            v.x = -moveSpeedCap + 0.01f;
            playerRB.velocity = v;
        }

        if (playerRB.velocity.z > moveSpeedCap && playerRB.velocity.z > 0) //forward
        {
            Vector3 v = playerRB.velocity;
            v.z = moveSpeedCap - 0.01f;
            playerRB.velocity = v;
        }

        if (playerRB.velocity.z < -moveSpeedCap && playerRB.velocity.z < 0) //backward
        {
            Vector3 v = playerRB.velocity;
            v.z = -moveSpeedCap + 0.01f;
            playerRB.velocity = v;
        }

        //jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            pickJumpSound();
            float actualJumpForce = jumpForce * 0.1f;
            
            Vector3 v = playerRB.velocity;
            v.y = Mathf.Sqrt(actualJumpForce * -2f * gravity);
            playerRB.velocity = v;

        }

        if(!isGrounded)
        {
            wasAirborne = true;
        }

        void pickJumpSound()
        {
            int num = Random.Range(1, 3);
            if (num == 1)
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(marioJump1);
            } 

            if (num == 2)
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(marioJump2);
            }

        }

        if (wasAirborne && isGrounded)
        {
            marioLandSound();
        }

        void marioLandSound()
        {
                wasAirborne = false;
                gameObject.GetComponent<AudioSource>().PlayOneShot(marioLand);
                
        }

        
        //isMoving checker
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputZ = Input.GetAxisRaw("Vertical");

        if (moveInputX == 0 && moveInputZ == 0)
        {
            isMoving = false;
        } else
        {
            isMoving = true;
        }

        if (moveInputX > 0)
        {
            movingLeft = true;
        } else
        {
            movingLeft = false;
        }


        if (moveInputX < 0)
        {
            movingRight = true;
        } else
        {
            movingRight = false;
        }


        if (moveInputZ < 0)
        {
            movingBackward = true;
        } else
        {
            movingBackward = false;
        }


        if (moveInputZ > 0)
        {
            movingForward = true;
        } else
        {
            movingForward = false;
        }
        

       


        /*
        if (mario.transform.rotation.eulerAngles.y == 0 && moveInput.x < 0)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 180;
            mario.transform.rotation = Quaternion.Euler(rotationVector);
            Debug.Log("F");
        }
        */
        /*
        if(!backArm.flipX & !frontArm.flipX & !body.flipX & !head.flipX & !frontFoot.flipX & !backFoot.flipX)
        {
            spriteNotFlipX = true;
        }

        if(spriteNotFlipX = true && moveInput.x < 0)
        {

        }
        */
    }
}
/*
 * //Fix gravity when falling off objects
        //have mario under constant gravity, when jumping jsut apply a force to him OR have his gravity be set to "jump force" while a boolean variable chekcs that his airborne-ness is caused by a jump instead of just becoming airborne from falling

        //movement
        /*
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //"move" is a variable that takes the value of the players movement speed on both the x and z axis
        

 
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize(); //prevent going diagonal from giving double speed

        playerRB.velocity = new Vector3(moveInput.x * moveSpeed, playerRB.velocity.y, moveInput.y * moveSpeed);
        */


/*
    RaycastHit hit;
    if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, groundCheckDistance, groundMask)) //from ground point, points directly down, send information from raycast to "hit", length of raycast, what to look for 
    {
        isGrounded = true;
    }
    else
    {
        isGrounded = false;
    }
    */

//movement
/*
isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //check if the player is touching the ground:

if (isGrounded && velocity.y =< 0)
{
    velocity.y = gravity; //if the player is on the ground and has no vertical velocity, set vertical velocity to -2 to prevent exponential velocity gain
}

//Gravity
if (isGrounded && velocity.y < 0)
{
    velocity.y = gravity; 
}
//float jumpForce2 = Mathf.Sqrt(jumpForce * -2f * gravity);

float x = Input.GetAxis("Horizontal");
float z = Input.GetAxis("Vertical");


Vector3 move = transform.right * x  + transform.forward * z; //"move" is a variable that takes the value of the players movement speed on both the x and z axis


float actualSpeed = moveSpeed / 1000; //normal movespeed is way too fast in the inspector so this makes it easier to change it, instead of having to specify a speed of 0.5, you can do 50
controller.Move(move * actualSpeed);


if (Input.GetButtonDown("Jump") && isGrounded)
{

    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
    //gameObject.GetComponent<AudioSource>().PlayOneShot(jumpSound); //sound of jumping
}



velocity.y += gravity * Time.deltaTime;

controller.Move(velocity * Time.deltaTime);


//public float airSpeedMod = 1f;
*/

/*
//basic movement
moveInput.x = Input.GetAxis("Horizontal");
moveInput.y = Input.GetAxis("Vertical");
moveInput.Normalize(); //prevent going diagonal from giving double speed

playerRB.velocity = new Vector3(moveInput.x * moveSpeed, playerRB.velocity.y, moveInput.y * moveSpeed);

//groundCheck
RaycastHit hit;
if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, groundCheckDistance, groundMask)) //from ground point, points directly down, send information from raycast to "hit", length of raycast, what to look for 
{
   isGrounded = true;
} else
{
   isGrounded = false;
}


//Gravity

if (isGrounded && velocity.y < 0)
{
   velocity.y = gravity;
}


velocity.y += gravity * Time.deltaTime;
controller.Move(velocity * Time.deltaTime);

//Jumping
if (Input.GetButtonDown("Jump") && isGrounded)
{
   // playerRB.velocity += new Vector3(0f, jumpForce, 0f);
   float jumpForce2 = Mathf.Sqrt(jumpForce * -2f * gravity);
   playerRB.AddForce(Vector3.up * jumpForce2, ForceMode.Impulse);
}



*/
