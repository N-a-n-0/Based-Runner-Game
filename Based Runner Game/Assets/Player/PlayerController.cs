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
    public float laneDistance = 2.5f;//The distance between tow lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float gravity = -12f;
    public float jumpHeight = 2;
    private Vector3 velocity;

    public Animator animator;
    private bool isSliding = false;

    public float slideDuration = .1f;

    


    bool toggle = false;

    void Start()
    {
        PlayerModel = this.gameObject;
        print(PlayerModel);
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1.2f;
    }

    private void FixedUpdate()
    {
        
        if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
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

        if (CutsceneEnter.powerupVar_PlayerController == false)
        {



            // print(slide);

            if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
                return;
            //Increase speed if true
            if (forwardSpeed < maxSpeed)
                forwardSpeed += 0.1f * Time.deltaTime;

            animator.SetBool("isGameStarted", true);
            move.z = forwardSpeed;

            isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);

            // print(slideCannotHappen + "SLIDE");
            // print(jumpCannotHappen + "JUMP");

            animator.SetBool("isGrounded", isGrounded);
            if (isGrounded && velocity.y < 0)
                velocity.y = -1f;

            if (isGrounded)
            {
                // slideCannotHappen = true;
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || SwipeManager.swipeUp)
                {

                    // StopCoroutine(Slide());
                    // if(jumpCannotHappen == false)
                    // {
                    //StopCoroutine(slide);
                    slideCannotHappen = true;
                    Jump();
                    // }



                }



                if (SwipeManager.swipeDown || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {


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
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || SwipeManager.swipeRight)
            {
                desiredLane++;
                if (desiredLane == 3)
                    desiredLane = 2;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || SwipeManager.swipeLeft)
            {
                desiredLane--;
                if (desiredLane == -1)
                    desiredLane = 0;
            }

            //Calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (desiredLane == 0)
                targetPosition += Vector3.left * laneDistance;
            else if (desiredLane == 2)
                targetPosition += Vector3.right * laneDistance;

            //transform.position = targetPosition;
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
                // controller.Move(diff);
            }

            controller.Move(move * Time.deltaTime);
        }
        else
        {
            print("POWERUP ANIMATION IS CURRENTLY HAPPENING");
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
        controller.center = controller.center = new Vector3(0, 0.2f, 0); 
        controller.height = 2;
        isSliding = false;
        slideCannotHappen = false;
        velocity.y = Mathf.Sqrt(jumpHeight * 5 * -gravity);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            FindObjectOfType<AudioManager>().StopSound("MainTheme");
            PlayerManager.gameOver = true;
           FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }

    }

    private IEnumerator Slide()
    {
       
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.2f, 0);
        controller.height = 1;
        
       
        yield return new WaitForSeconds(1.75f);
        slide = null;
        isSliding = false;
        animator.SetBool("isSliding", false);

        controller.center = new Vector3(0,0.2f,0);
        controller.height = 2;

        jumpCannotHappen = false;
       
    }
}