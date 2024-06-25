using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGame : MonoBehaviour
{
    float curSpeed = 25;
    float savedSpeed = 25;

    public Animator animator;


    public Camera MainCamera;

    public Camera MicroGameCamera;

    public bool alreadyEntered = false;// Used this bool to fix the issues we were having with speed

    public static bool StartMiniGame = false; //I may want to use this throughout many scripts where the activation of microgames are dependent on this static bool var

    public static float timeNeeded = 3f; //I wanna possibly give each microminigame its own custom timer using this var that we can just reference and change 

    void Update()
    {
        curSpeed = PlayerController.forwardSpeed;
      //  Debug.Log("CURSPEED " + curSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {

        //The reason you had that speed error is because you had PlayerController.forwardSpeed = 0; outside of the if statement and you didn't have any conditions stopping the player from retriggering the OnTriggerEnter

        if (other.CompareTag("Player") && alreadyEntered == false)
        {
            print("Entered this minigame");
            StartMiniGame = true;
            //Make sure you can't retrigger this if statement using this bool
            alreadyEntered = true;

            MainCamera.enabled = false;
            MicroGameCamera.enabled = true;
            Debug.Log("Collision: CURSPEED " + curSpeed);
            animator.SetBool("Touched", true);


            //moved these  lines right here in the if statement 
            savedSpeed = curSpeed;
            PlayerController.forwardSpeed = 0;

            StartCoroutine(returnSpeedPlz());
        }
       
    }

    private IEnumerator returnSpeedPlz()
    {
        yield return new WaitForSeconds(timeNeeded);
        MainCamera.enabled = true;
        MicroGameCamera.enabled = false;
        PlayerController.forwardSpeed = savedSpeed;
    }
}
