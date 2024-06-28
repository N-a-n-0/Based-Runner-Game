using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MicroGameManager : MonoBehaviour
{
    public static float curSpeed = 25;
    public float savedSpeed = curSpeed;

    public static bool alreadyEntered = false;// Used this bool to fix the issues we were having with speed


    public static int timeNeeded = 5; //I wanna possibly give each microminigame its own custom timer using this var that we can just reference and change 

    public static int VisibleTimer = timeNeeded;


    public GameObject MicroGameObj_1;

    public MicroMath MiniG_1;

    public IEnumerator ActiveCoroutine = null;

    void Update()
    {

       

        if (alreadyEntered == true && CameraFunctions.MicroGameEndReached == true && ActiveCoroutine == null)
        {


            ActiveCoroutine = returnSpeedPlz();


            StartCoroutine(ActiveCoroutine);

            
        }
    }

  
    private IEnumerator returnSpeedPlz()
    {

        print("Entered this minigame");


        //moved these  lines right here in the if statement 
        savedSpeed = curSpeed;


        CutsceneEnter.MainCamera_Reference.enabled = false;
        MicroGameObj_1.SetActive(true);
         
       
       VisibleTimer =  timeNeeded;
      
        
        for (int i = timeNeeded; i > 0; i--)
        {
           
            VisibleTimer--;
            yield return new WaitForSeconds(1);
            print( VisibleTimer + "Second has passed");
        }
        MiniG_1.RemoveListeners();
        MiniG_1.GenerateAnswer();
        MicroGame.MicroCamRef.enabled = false;
        
        CutsceneEnter.MainCamera_Reference.enabled = true;
      //  MicroGameCamera.enabled = false;
        PlayerController.forwardSpeed = savedSpeed;
        CameraFunctions.MicroGameEndReached = false;

        MicroGameObj_1.SetActive(false);
        
        // MicroGM.MicroGameParentObj[0].SetActive(false);
        ActiveCoroutine = null;
        alreadyEntered = false;
    }
}
