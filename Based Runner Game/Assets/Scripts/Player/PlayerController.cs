using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;


    public static float forwardSpeed = 25;
    public static float maxSpeed = 75;

    public static GameObject PlayerModel  =  null;

    public static float jumpTimer;
    public int jumpNumber;

    public GameObject childPlayerModel;

    public static GameObject child_Obj_Reference; //Might be useless look into this

    //PLAYER MODEL THAT THE PLAYER WILL CONTROL THROUGHOUT THE GAME
    public  GameObject buttmanGameObj;
    public static GameObject buttmanGameObjRef;

    //THIS MODEL IS ONLY FOR SPECIAL ANIMTAIONS THAT WE DONT REALLY NEED ATTACHED TO OUR MAIN PLAYER MODEL
    public GameObject NonPlayerableButtManModel;
    public static GameObject NonPlayerableButtManModelRef;

    private IEnumerator slide;

    public static int desiredLane = 1;//0:left, 1:middle, 2:right
    public const float  laneDistance = 2.5f;//The distance between tow lanes

    public static bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float gravity = -12f;
    public int firstJumpHeight, secondJumpHeight, thirdJumpHeight;
    public float jumpHeight = 5;
    private Vector3 velocity;

    [SerializeField]
    public static Animator animator;
    public static bool isSliding = false;
   
    public float slideDuration = .1f;


    public bool toggle = false;

    


    void Start() 
    {
        EndOfLevel.gameWasBeaten = false;
        buttmanGameObjRef = buttmanGameObj;
        NonPlayerableButtManModelRef = NonPlayerableButtManModel;

        desiredLane = 1;

        //****** ALL THESE VALUES IN BETWEEN THESE TWO COMMENTS SET THE POWERUP VARIBLES TO THEIR DEAFULTS 
        PlayerFunctions.powerUpAnimFinished = false;
        CutsceneEnter.PowerApplies = false;
        CutsceneEnter.powerUpChecker = false;
        PlayerFunctions.powerUpAnimation = false;
        PlayerFunctions.powerUp_In_Progress = null; 
        //****** THIS SOLVES THE POWERUP BUG WHERE A PLAYER LEAVES MID GAME

        child_Obj_Reference = returnChildGameObject();
        animator = childPlayerModel.GetComponent<Animator>();
        print(animator);
        forwardSpeed = 20;
        maxSpeed = 45;
        PlayerModel = this.gameObject; //make a different object that only stores the render object of the player so we can cut out the player when we need to
        print(PlayerModel);
        controller = GetComponent<CharacterController>();
      //  Time.timeScale = 1.25f;

      //  childPlayerModel.transform.localScale = new Vector3(.5f, .5f, .5f);


    }

    private void FixedUpdate() //handles physics 
    {
        //Increase Speed
        if (toggle)
        {
            toggle = false;
            if (forwardSpeed < maxSpeed)
            { forwardSpeed += 0.1f * Time.fixedDeltaTime; }
              //  forwardSpeed += 0.1f * Time.fixedDeltaTime;

        }
        else
        {
           
            toggle = true;
           // if (Time.timeScale < 2f)
              //  Time.timeScale += .001f * Time.fixedDeltaTime;


        }
        if (!PlayerManager.isGameStarted || PlayerManager.gameOver == false)
            return;

        
    }

    void Update()
    {
     //   print("Forward speed: " + forwardSpeed);
       // print("Time Scale: " + Time.timeScale);

        if (PlayerManager.gameOver == true)
        {
          //  print("forwardSpeed: " + forwardSpeed);
            animator.SetBool("Game Over", true);
            PlayerManager.gameOver = true;
        }

        if (CutsceneEnter.powerupVar_PlayerController == false  && PlayerManager.gameOver == false && MicroGameManager.alreadyEntered == false  && EndOfLevel.gameWasBeaten == false)
        {

          

           if (!PlayerManager.isGameStarted || PlayerManager.gameOver )
            {
             
                return;
            }

          //  print("GAMEOVER VALUE: " + PlayerManager.gameOver + "\n" + "powerupVar_PlayerController Value: " + CutsceneEnter.powerupVar_PlayerController); //Why does sometimes when the player gets a gameover they continue to move despite the if statement conditions?   
            //Increase speed if true
            //if (forwardSpeed < maxSpeed)
               // forwardSpeed += 0.1f * Time.deltaTime;

            animator.SetBool("isGameStarted", true);
            move.z = forwardSpeed;

            isGrounded = Physics.CheckSphere(groundCheck.position, .05f, groundLayer);

            if(isGrounded == true)
            {

                animator.SetBool("Jump", false);
                jumpTimer += Time.deltaTime;

                if (jumpNumber == 1 && jumpTimer >= .6)
                {
                    print("Checking for a double jump status");
                   
                   
                        jumpTimer = 0;
                        jumpNumber = 0;
                   
                }
                else if(jumpNumber == 2 && jumpTimer >= .6)
                {

                    print("Checking for a tirple jump status");
                   
                        jumpTimer = 0;
                        jumpNumber = 0;
                  
                }
            }
           

            animator.SetBool("isGrounded", isGrounded);
            if (isGrounded && velocity.y < 0)
                velocity.y = -1f;

            if (isGrounded && PlayerManager.gameOver == false && PlayerFunctions.powerUpAnimFinished == false)
            {
                
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || SwipeManager.swipeUp)
                {



                    // slideCannotHappen = true;
                   // PlayerFunctions.comboRatingNumber = 1;
                    isGrounded = false;
                    Jump();
                  



                }



                if (SwipeManager.swipeDown || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && PlayerManager.gameOver == false)
                {
                    print("UPDATE SLIDE");

                   
                    if (slide == null)
                    {
                        slide = Slide();
                        //  jumpCannotHappen = true;
                        StopAllCoroutines();
                        StartCoroutine(slide);
                    }





                }

            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
                if (SwipeManager.swipeDown || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
                {
                   
                    velocity.y = -35;
                }

            }
            controller.Move(velocity * Time.deltaTime);

            //Gather the inputs on which lane we should be
            if ( (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || SwipeManager.swipeRight) &&  PlayerManager.gameOver == false && PlayerFunctions.powerUpAnimFinished == false)
            {
                desiredLane++;
               
                slideCancel();
                if (isGrounded == true && animator.GetBool ("LaneRight") == false)
               {
                    animator.SetBool("LaneRight", true);
                }
               else
                {
                    animator.SetBool("LaneRight", false);
                }
                //CONSIDER THIS A GOOD SPOT FOR THE LANE CHANGE ANIMATION CODE ***************************************************************

                if (desiredLane == 3)
                {
                   
                    desiredLane = 2;

                }
              // animator.SetBool("LaneRight", false);
            }
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || SwipeManager.swipeLeft)  && PlayerManager.gameOver == false && PlayerFunctions.powerUpAnimFinished == false)
            {

                desiredLane--;
              
                slideCancel();
               if (isGrounded == true && animator.GetBool("LaneLeft") == false)
               {
                    animator.SetBool("LaneLeft", true);
               }
               else
                {
                    animator.SetBool("LaneLeft", false);
                }
                //CONSIDER THIS A GOOD SPOT FOR THE LANE CHANGE ANIMATION CODE ***************************************************************

                if (desiredLane == -1)
                {
                   
                    desiredLane = 0;
                }
              //  animator.SetBool("LaneLeft", false);
            }

            //Calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
           
            if (desiredLane == 0)
            {
             
                targetPosition += Vector3.left * laneDistance;

            }
               
            else if (desiredLane == 2)
            {
               
                targetPosition += Vector3.right * laneDistance;
            }
              

          
            if (transform.position != targetPosition)
            {
                Vector3 diff = targetPosition - transform.position;
                Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
             
                if (moveDir.sqrMagnitude < diff.magnitude)
                {
                   
                    controller.Move(moveDir);
                }
                
                else
                {
                
                    controller.Move(diff);
                }
               
            }

            controller.Move(move * Time.deltaTime);
        }
        else
        {
           // CutsceneEnter.powerupVar_PlayerController = false;
            //print("POWERUP ANIMATION IS CURRENTLY HAPPENING" + CutsceneEnter.powerupVar_PlayerController);
        }

    }

    private void Jump()
    {
       
        slideCancel();

        print(jumpTimer);
        switch (jumpNumber)
        {
            case 0:
                print(jumpTimer);
                jumpTimer = 0;
                print(jumpTimer);
                animator.SetBool("Jump", true);
                velocity.y = Mathf.Sqrt(jumpHeight * firstJumpHeight * -gravity);

                 
                jumpNumber++;
                break;
            case 1:

                jumpTimer = 0;
                animator.SetBool("Jump", true);
                velocity.y = Mathf.Sqrt(jumpHeight * secondJumpHeight * -gravity);
                

                jumpNumber++;
                break;
                case 2:
                jumpTimer = 0;
                animator.SetBool("Jump", true);
                velocity.y = Mathf.Sqrt(jumpHeight * thirdJumpHeight * -gravity);

                
                jumpNumber = 0;
                break;

        }
      //  if(jumpNumber == 0)
     //   {
     //       velocity.y = Mathf.Sqrt(jumpHeight * 5 * -gravity);
     //  }


     //   jumpTimer = Time.unscaledTime;
        //Make differenet types of jump animations here
      //  animator.SetBool("Jump", true);


        //  isSliding = false;
        //  slideCannotHappen = false;
     //   velocity.y = Mathf.Sqrt(jumpHeight * 5 * -gravity);


    }

    

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        



        if (hit.transform.tag == "Obstacle" && CutsceneEnter.PowerApplies == false)
        {
            FindObjectOfType<AudioManager>().StopSound("MainTheme");
            PlayerManager.gameOver = true;
           
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
        else if(hit.transform.tag == "Obstacle" && CutsceneEnter.PowerApplies == true)
        {
           
            Destroy(hit.gameObject);
        }

    }

    private IEnumerator Slide()
    {
       
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.4f, 0);
        controller.height = .6f;
        

       
        yield return new WaitForSeconds(1.75f);
        slide = null;
        isSliding = false;
        animator.SetBool("isSliding", false);

        controller.center = new Vector3(0,0,0);
        controller.height = 1.75f;

       // jumpCannotHappen = false;
       
    }
    //not this parent object but the child object that stores the visual model you see in game
    public  GameObject returnChildGameObject()
    {

        return childPlayerModel;
    }

    public static Animator getAnimator(GameObject obj)
    {
        return obj.GetComponent<Animator>();
    }


    public void slideCancel()
    {
        PlayerFunctions.comboRatingNumber = 1;
        isSliding = false;
        //if (slide != null)
        //  {
        print("A slide is happening");
            StopAllCoroutines();
            slide = null;
       
        animator.SetBool("isSliding", false);

            controller.center = controller.center = new Vector3(0, 0, 0);
            controller.height = 1.6f;
        //  }
        print(slide);
    }
}