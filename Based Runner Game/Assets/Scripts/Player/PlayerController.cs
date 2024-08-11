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

    public GameObject childPlayerModel;

    public static GameObject child_Obj_Reference; //Might be useless look into this

    private IEnumerator slide;

    public static int desiredLane = 1;//0:left, 1:middle, 2:right
    public const float  laneDistance = 2.5f;//The distance between tow lanes

    public static bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float gravity = -12f;
    public float jumpHeight = 5;
    private Vector3 velocity;

    [SerializeField]
    public static Animator animator;
    public static bool isSliding = false;
   
    public float slideDuration = .1f;


    public bool toggle = false;

    


    void Start() 
    {
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
        PlayerModel = this.gameObject;
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

        if (CutsceneEnter.powerupVar_PlayerController == false  && PlayerManager.gameOver == false && MicroGameManager.alreadyEntered == false )
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
        animator.SetBool("Jump", true);


        //  isSliding = false;
        //  slideCannotHappen = false;
        velocity.y = Mathf.Sqrt(jumpHeight * 5 * -gravity);


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