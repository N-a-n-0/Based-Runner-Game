using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MicroGameTemplate
{
    public string name;
    public int miniGameTime;
    public GameObject MicroGameObj;
}


public class MicroGameManager : MonoBehaviour
{
    public MicroGameTemplate[] microGameTemplates;

    public static float curSpeed = 25;
    public float savedSpeed = curSpeed;

    public static bool alreadyEntered = false;// Used this bool to fix the issues we were having with speed

    public static int VisibleTimer;


    public MicroMath MiniG_1;
     
    public IEnumerator ActiveCoroutine = null;

    public static bool timeRanOut;

    public static int forloopIndex;

    void Start()
    {
         
    }

    void Update()
    {

        if (alreadyEntered == true && CameraFunctions.MicroGameEndReached == true && ActiveCoroutine == null)
        {


            ActiveCoroutine = MathMicrogame_0(); //maybe make the 0 part of the function name be some kind of int varaible to make it easier to call Ienumerators EXAMPLE: "MathMicrogame_" + MiniGame_ID rather than making a new if statement 


            StartCoroutine(ActiveCoroutine);

        }
    }

  
    private IEnumerator MathMicrogame_0()
    {

        print("Entered this minigame");

        savedSpeed = curSpeed;


        CutsceneEnter.MainCamera_Reference.enabled = false;
        microGameTemplates[0].MicroGameObj.SetActive(true);
         
       
       VisibleTimer = microGameTemplates[0].miniGameTime;
      

        //need to figure out a way to cutoff the MicroGame once the player gets the correct answer. Maybe another animation? Maybe use code from MicroMath?

        for (forloopIndex = VisibleTimer; forloopIndex > 0; forloopIndex--) // this will probably need to be  awhile loop and we should run the timer off of something else we need to find a way to make sure the time doesnt overwrite if last sec
        {
            print(VisibleTimer + "Seconds left");
            VisibleTimer--;
            yield return new WaitForSeconds(1);
            
        }
        timeRanOut = true;
 
            MiniG_1.RemoveListeners();
            MiniG_1.GenerateAnswer();

            MicroGame.MicroCamRef.enabled = false;
            CutsceneEnter.MainCamera_Reference.enabled = true;

            PlayerController.forwardSpeed = savedSpeed;
            CameraFunctions.MicroGameEndReached = false;

            microGameTemplates[0].MicroGameObj.SetActive(false);


            ActiveCoroutine = null;
            alreadyEntered = false;
     
       
    }
}
