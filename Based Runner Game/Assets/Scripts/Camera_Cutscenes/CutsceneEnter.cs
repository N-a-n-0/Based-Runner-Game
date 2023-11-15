using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter : MonoBehaviour
{

    public static bool powerUpChecker = false;
    public static bool powerupVar_PlayerController = false;

    public static bool powerUpCheckerParent = false;

    public static bool PowerApplies = false;

    [SerializeField]
   
    public   Camera MainCamera;
    public  Camera cutsceneCam;

    public Camera FirstPersonCamera;

    public Animator animator;

    public float PlayersScale = 6f;


    public static IEnumerator CutSceneFunction = null;


    public static float CurrentForwardSpeed = 25;

    void Start()
    {
        powerUpChecker = false;
        CutSceneFunction = null;
        PlayersScale = 6f;
        powerupVar_PlayerController = false;
        CurrentForwardSpeed = 25;
    }

    IEnumerator FinishCut()
    {
        print("ITS ABOUT TO WAIT");
        PowerApplies = true;
        yield return new WaitForSeconds(1);
        animator.SetBool("PowerUp", false);
        yield return new WaitForSeconds(4);
       
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

      
       
        while (PlayersScale > 1f == true)
        {
            print("PLAYER IS SCALING BACK TO NORMAL");
            
            yield return new WaitForSeconds(10 * Time.deltaTime);
            PlayersScale -= .1f;
            print(PlayersScale);

            if(PlayersScale <= 1f)
            {
                PlayersScale = 1f;
            }

            PlayerController.PlayerModel.transform.localScale = new Vector3(PlayersScale, PlayersScale, PlayersScale);
        }
        //set values back to the player
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



    }

    void Update()
        {
     
       
        if(powerUpChecker == true && CutSceneFunction == null)
        {
            CutSceneFunction = FinishCut();
            animator.SetBool("PowerUp", true);
            MainCamera.enabled = false;
            cutsceneCam.enabled = true;

           
            print("TRIGGERED");
            StartCoroutine(CutSceneFunction);
           
        }
      
    }
}
