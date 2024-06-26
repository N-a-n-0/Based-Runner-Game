using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGame : MonoBehaviour
{

    public Animator animator;
    public Camera MicroGameCamera;
    public static Camera MicroCamRef;



    private void OnTriggerEnter(Collider other)
    {
        
        //The reason you had that speed error is because you had PlayerController.forwardSpeed = 0; outside of the if statement and you didn't have any conditions stopping the player from retriggering the OnTriggerEnter

        if (other.CompareTag("Player") && MicroGameManager.alreadyEntered == false)
        {


            print("Entered this minigame");
            MicroCamRef = MicroGameCamera;


            MicroGameManager.alreadyEntered = true;
            MicroGameCamera.enabled = true;
            animator.SetBool("Touched", true);


            MicroGameManager.curSpeed = PlayerController.forwardSpeed;
            PlayerController.forwardSpeed = 0;

            print("CURSPEED:" + MicroGameManager.curSpeed);

            print("PLAYERCONTROLLER: " + PlayerController.forwardSpeed);
        }
       
    }

   
}
