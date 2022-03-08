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
    public float gravity = -0.9f;
    public float spaceReleaseGravity = -2f;

    public float jumpForce;
    [Tooltip("The value that movement input is multiplied by when Mario is airborne. Default: 100")]
    public float airborneMoveSpeed = 100f;
    [Tooltip("The maximum speed Mario can move at. If he goes over this limit, his speed is readjusted to 0.01 lower in that direction")]
    public float moveSpeedCap = 5.01f;

    private Vector2 moveInput;
    public float groundDistance; //size of the sphere
    public LayerMask groundMask;
    public Transform groundPoint;

    //Jump Physics
    private Vector3 jumpVelocity;


    //Checker variables
    Vector3 velocity;
    public bool isGrounded;
    public bool isMoving;
    public bool movingLeft;
    public bool movingRight;
    public bool movingBackward;
    public bool movingForward;

    private bool wasAirborne = false;
    public bool spaceReleased = false;




    private int frameCount = 1;
    public int frameCountCap = 20;

    private float timer = 0f;
    public float timerCap = 0.5f; //e.g 5 is 5 seconds

    public bool hasLanded = false;
    

    //Player 
    public GameObject player;
    public Rigidbody playerRB;

    //Dust
    public GameObject dust;
    private DustAnimation dustAnimScript;

    private void Start()
    {
        dustAnimScript = dust.GetComponent<DustAnimation>();
    }

    public int bread = 5;

    



    private void FixedUpdate()
    {
        //frame counter
        frameCount++;
        if (frameCount == frameCountCap)
        {
            frameCount = 1;

        }

        timer += Time.deltaTime;

        if (timer > timerCap)
        {
            timer = 0;
        }


        if (!isGrounded)
        {
            playerRB.AddRelativeForce(0, gravity, 0, ForceMode.Impulse); //change player mass to adjust jump feel, 1.5 seems good
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
            float actualAirborneMoveSpeed = airborneMoveSpeed / 100;

            if (timer == 0) //to limit how quickly this force is applied, its applied only every 10 frames
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
        
        

        //jumping physics
        float actualJumpForce = jumpForce * 0.1f;
        jumpVelocity = playerRB.velocity;
        jumpVelocity.y = Mathf.Sqrt(actualJumpForce * -2f * gravity);
         
    }

    void Update()
    {
        //check for jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            pickJumpSound();
            playerRB.velocity = jumpVelocity;
        }

        if (Input.GetButtonUp("Jump") && !isGrounded && !spaceReleased) //enables releasing space early for mini jump
        {
            Vector3 v = playerRB.velocity;
            v.y = 0;
            playerRB.velocity = v;

            spaceReleased = true;

            //TODO: Make this bound by a check so this can only happen if the cause of moving is a jump, i.e logic from the jumping bit
        }


        //groundCheck with sphere
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
            //velocity.y += gravity * Time.deltaTime; 
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

        
        if (!isGrounded)
        {
            wasAirborne = true;
        }
        
        if (wasAirborne && isGrounded)
        {
            marioLandSound();
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
        
    }

    public void pickJumpSound()
    {
        int num = Random.Range(1, 1);
        if (num == 1)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(marioJump1);
        }

        if (num == 2)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(marioJump2);
        }

        //if you want more jump sounds, increase the range of random and add more if's for each sound
    }

    public void marioLandSound()
    {
        gravity = -0.9f;
        spaceReleased = false;
        wasAirborne = false;
        gameObject.GetComponent<AudioSource>().PlayOneShot(marioLand);

        dustAnimScript.PlayLandAnim();
    }
}
