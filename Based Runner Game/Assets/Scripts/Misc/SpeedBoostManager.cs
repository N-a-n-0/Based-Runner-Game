using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpeedBoostManager : MonoBehaviour
{
    public static SpeedBoostManager instance;

    public static bool speeedApplied;


    public static int currentTimer;
    public static int Flag;

    private static float savedSpeed;

    public static IEnumerator currentFunction = null;
    public void Start()
    {
        savedSpeed = PlayerController.forwardSpeed;
        speeedApplied = false;
        currentFunction = null;
    }

    private void Awake()
    {

        // Ensure there's only one instance of GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplySpeedBoost(float speedBuff, int duration)
    {

        currentTimer = 0;
        Flag = duration;
        // Start the speed boost routine
        if (currentFunction != null)
        {
           
            
        }

        if(currentFunction == null)
        {
           
            if(speeedApplied == false)
            {
                savedSpeed = PlayerController.forwardSpeed;
                speeedApplied = true;
                PlayerController.forwardSpeed += speedBuff;

                //the saved speed for some reason only saves the first time it saves and it only returns that value so if I first start the trigger with a speed of 25 it will return it to that much no matter how much I chang eit 
                currentFunction = SpeedBoostRoutine(speedBuff, duration);

                StartCoroutine(currentFunction);
            }
           
           
        }
       
    }

    private IEnumerator SpeedBoostRoutine(float speedBuff, int duration)
    {
        // Assuming the player has a script with a speed variable that you handle
        

        while (currentTimer < Flag)
        {
            print("currentTimer" + currentTimer);
            while (MicroGameManager.alreadyEntered == true || (PlayerFunctions.powerUpAnimation == false && PlayerFunctions.powerUp_In_Progress != null))
            {
               // if (PlayerFunctions.stopPlayerMovement == true)
              //  {
             //      PlayerController.forwardSpeed = 0;
              //  }
               

                yield return null;
            }

        

            yield return new WaitForSeconds(1f);
            currentTimer++;

            

        }
        
      //  currentTimer = 0;
        
        speeedApplied = false;
        PlayerController.forwardSpeed = savedSpeed; // Revert to default speed
      //  StopCoroutine(currentFunction);
        currentFunction = null;
    }
}
