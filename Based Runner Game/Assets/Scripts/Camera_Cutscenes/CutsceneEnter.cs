using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter : MonoBehaviour
{

    public static bool powerUpChecker = false;
    public static bool powerupVar_PlayerController = false;

    // public static bool powerUpCheckerParent = false;

    public static bool PowerApplies = false;


    public Camera MainCamera;
    public Camera cutsceneCam;

    public Camera FirstPersonCamera;

    public Animator animator;

    //REFERENCES BELOW
    public static Animator child_Animator_Reference;

    public static Camera MainCamera_Reference;

    public static Camera cutsceneCam_Reference;

    public static Camera FirstPersonCamera_Reference;

    //______________________________________________

    public static float PlayersScale = 6f;



    public static IEnumerator CutSceneFunction = null;


    public static float CurrentForwardSpeed = 25;

    void Start()
    {
        MainCamera_Reference = getCamera(MainCamera);
        cutsceneCam_Reference = getCamera(cutsceneCam);
        FirstPersonCamera_Reference = getCamera(FirstPersonCamera);


        child_Animator_Reference = returnAnimator();
        powerUpChecker = false;
        CutSceneFunction = null;
        PlayersScale = 6f;
        powerupVar_PlayerController = false;
        CurrentForwardSpeed = 25;

        print(MainCamera_Reference);
        print(cutsceneCam_Reference);
        print(FirstPersonCamera_Reference);

    }


    /*
    IEnumerator FinishCut()
    {
        print("ITS ABOUT TO WAIT");
        PowerApplies = true;
        yield return new WaitForSeconds(1); // one second will pass here
        animator.SetBool("PowerUp", false);
        yield return new WaitForSeconds(4); // four seconds will pass here 
       
        PlayerController.forwardSpeed = CurrentForwardSpeed;
        //Speed is being saved here

        cutsceneCam.enabled = false;
        FirstPersonCamera.enabled = true;
        PlayerController.maxSpeed = 75;


       

        powerupVar_PlayerController = false;
       

        yield return new WaitForSeconds(10);

        print("10 SECONDS HAVE PASSED");



         PlayerController.PlayerModel.transform.position += new Vector3(0, 2f, 0);
        PlayerController.forwardSpeed = 0;
        PlayerController.maxSpeed = 0;

        PlayerController.forwardSpeed = CurrentForwardSpeed;
        PlayerController.maxSpeed = 75;
        powerUpChecker = false;
        powerupVar_PlayerController = false;
         PlayersScale = 6f;
      
        CutSceneFunction = null;
        PowerApplies = false;

        FirstPersonCamera.enabled = false;
        MainCamera.enabled = true;
        CharacterController controller = CharacterSelector.CurrentSelectedCharacter.GetComponent<CharacterController>();
        controller.radius = .2f;
        StopAllCoroutines();



    }*/

    void Update()
    {

        /*
         if(powerUpChecker == true && ProgressBar.targetProgress >= 1)
         {
            // CutSceneFunction = FinishCut();
            // animator.SetBool("PowerUp", true);
             MainCamera.enabled = false;
             cutsceneCam.enabled = true;


             print("TRIGGERED");
             //StartCoroutine(CutSceneFunction);
           //  PlayerFunctions.playerPowerUp();

         }*/

    }


    //RETURN FUNCTIONS



    public Animator returnAnimator()
    {
        return animator;
    }

    public Camera getCamera(Camera cam)
    {
        return cam;
    }
}
