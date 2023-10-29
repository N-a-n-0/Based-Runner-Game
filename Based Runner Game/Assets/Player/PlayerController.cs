using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    public static float forwardSpeed = 25;
    public static float maxSpeed = 75;

    public static GameObject PlayerModel  =  null;

    
    


    private bool jumpCannotHappen;
    private bool slideCannotHappen;

    private IEnumerator slide;

    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public const int  laneDistance = 4;//The distance between tow lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float gravity = -12f;
    public float jumpHeight = 2;
    private Vector3 velocity;

    public Animator animator;
    private bool isSliding = false;

    public float slideDuration = .1f;

    //public static bool storedPowerUpValue = CutsceneEnter.powerupVar_PlayerController;


    bool toggle = false;

    void Start()
    {
        forwardSpeed = 25;
        maxSpeed = 45;
        PlayerModel = this.gameObject;
        print(PlayerModel);
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1f;
        //storedPowerUpValue = false;
    }

    private void FixedUpdate()
    {
        
        if (!PlayerManager.isGameStarted || PlayerManager.gameOver == false)
            return;

        //Increase Speed
        if (toggle)
        {
            toggle = false;
           if (forwardSpeed < maxSpeed)
                forwardSpeed += 0.1f * Time.fixedDeltaTime;
          //  print(forwardSpeed + "FORWARD SPEED");
        }
        else
        {
           toggle = true;
           if (Time.timeScale < 2f)
             Time.timeScale += 0.005f * Time.fixedDeltaTime;

          //  print(Time.timeScale + "TIMESCALE");
            //Time.timeScale = 2f;
        }
    }

    void Update()
    {
        if(PlayerManager.gameOver == true)
        {
            animator.SetBool("Game Over", true);
        }

        if (CutsceneEnter.powerupVar_PlayerController == false && PlayerManager.Final_GameOver_Check == false)
        {

          //  print("UPDATE START");

            // print(slide);

           if (!PlayerManager.isGameStarted || PlayerManager.gameOver )
            {
             //   print("UPDATE FIRST");
                return;
            }
               
            //Increase speed if true
            if (forwardSpeed < maxSpeed)
                forwardSpeed += 0.1f * Time.deltaTime;

            animator.SetBool("isGameStarted", true);
            move.z = forwardSpeed;

            isGrounded = Physics.CheckSphere(groundCheck.position, .05f, groundLayer);

            // print(slideCannotHappen + "SLIDE");
            // print(jumpCannotHappen + "JUMP");

            animator.SetBool("isGrounded", isGrounded);
            if (isGrounded && velocity.y < 0)
                velocity.y = -1f;

            if (isGrounded && PlayerManager.Final_GameOver_Check == false)
            {
                // slideCannotHappen = true;
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || SwipeManager.swipeUp)
                {

                    // StopCoroutine(Slide());
                    // if(jumpCannotHappen == false)
                    // {
                    
                    slideCannotHappen = true;
                    Jump();
                    // }



                }



                if (SwipeManager.swipeDown || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && PlayerManager.Final_GameOver_Check == false)
                {
                    print("UPDATE SLIDE");

                    //  StopCoroutine(slide);
                    if (slide == null)
                    {
                        slide = Slide();
                        jumpCannotHappen = true;
                        StartCoroutine(slide);
                    }





                }

            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
                if (SwipeManager.swipeDown || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) //&& !isSliding && animator.GetBool("isSliding") == false
                {
                    // slide = Slide();
                    //   StartCoroutine(slide);
                    velocity.y = -10;
                }

            }
            controller.Move(velocity * Time.deltaTime);

            //Gather the inputs on which lane we should be
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || SwipeManager.swipeRight &&  PlayerManager.Final_GameOver_Check == false)
            {
                desiredLane++;
                if (desiredLane == 3)
                    desiredLane = 2;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || SwipeManager.swipeLeft  && PlayerManager.Final_GameOver_Check == false)
            {
                desiredLane--;
                if (desiredLane == -1)
                    desiredLane = 0;
            }

            //Calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
           // print(targetPosition);
            if (desiredLane == 0)
            {
               // print("MOVED LEFT HAPPEN");
                targetPosition += Vector3.left * laneDistance;

            }
               
            else if (desiredLane == 2)
            {
              //  print("MOVED RIGHT HAPPEN");
                targetPosition += Vector3.right * laneDistance;
            }
              

            //transform.position = targetPosition;
            if (transform.position != targetPosition)
            {
                Vector3 diff = targetPosition - transform.position;
                Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
               // print("move DIR" + moveDir.sqrMagnitude);
                if (moveDir.sqrMagnitude < diff.magnitude)
                {
                    controller.Move(moveDir);
                }
                
                else
                {
                    controller.Move(diff);
                }
                // controller.Move(diff);
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
       
       if(slide != null)
        {
            print("A slide is happening");
            StopAllCoroutines();
            slide = null;
           //  animator.SetBool("isSliding", false);
            

        }
      
        animator.SetBool("isGrounded", true);
        animator.SetBool("isSliding", false);
        controller.center = controller.center = new Vector3(0, 0, 0); 
        controller.height = 2;
        isSliding = false;
        slideCannotHappen = false;
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

        jumpCannotHappen = false;
       
    }
}