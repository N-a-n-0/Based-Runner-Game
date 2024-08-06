using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGame : MonoBehaviour
{

    public Animator animator;
    public Camera MicroGameCamera;
    public static Camera MicroCamRef;

     

    public string objectTag;
    void Start()
    {
          objectTag = gameObject.tag;
    }
    

    private void OnTriggerEnter(Collider other)
    {

        //The reason you had that speed error is because you had PlayerController.forwardSpeed = 0; outside of the if statement and you didn't have any conditions stopping the player from retriggering the OnTriggerEnter
        
        print("MicroGameManager.alreadyEntered" + MicroGameManager.alreadyEntered);

        if (other.CompareTag("Player") && MicroGameManager.alreadyEntered == false && CutsceneEnter.PowerApplies == false)
        {

            ChangePlayerValues();


            switch (objectTag) //we are looking for the trigger objects Tag to see which MicroMiniGame we should run here!
            {
                case "MicroMathGame": //number 0
                    MicroGameManager.selectedMicroGame = 0;
                    break;


                case "MicroGameTouchButton": //number 1
                   // MicroGameManager.UniversalObjectiveTextObjectReference.SetActive(true);
                    MicroGameManager.selectedMicroGame = 1;
                    break;
                default:
                    print("coulnd't find a function with that name SORRY");
                    break;
            }

           
        }
       
    }

   public void ChangePlayerValues()
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
