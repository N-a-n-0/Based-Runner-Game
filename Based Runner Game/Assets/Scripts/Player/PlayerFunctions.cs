using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerFunctions : MonoBehaviour
{
    public static IEnumerator powerUp_In_Progress = null;

    public static bool powerUpAnimFinished = false;

    public TMP_Text comboText;
    public static TMP_Text comboText_Reference;
    public static string comboRating = "";
    public static int comboRatingNumber = 0;


    public static bool powerUpAnimation = false;

     


    public void Start()
    {
        comboText_Reference = comboText;
    }

    public void Update()
    {
       
        if (CutsceneEnter.powerUpChecker == true && powerUp_In_Progress == null) //THIS IS WHERE YOU LEFT OFF REMEMBER THAT OKAY 
        {
          //  StartCoroutine(scaleUp_scaleDown(5, 1));
            powerUp_In_Progress = playerPowerUp();
            StartCoroutine(powerUp_In_Progress);
        }
    }

  public void IncreasePlayerCharacterController()
    {
        print("TESTING PLAYER FUNCTION SCRIPT");
        print(CharacterSelector.CurrentSelectedCharacter);
        CharacterController controller = CharacterSelector.CurrentSelectedCharacter.GetComponent<CharacterController>();
        controller.radius = 0.7f;
    }

    public void DecreasePlayerCharacterController()
    {
        print("TESTING PLAYER FUNCTION SCRIPT");
        print(CharacterSelector.CurrentSelectedCharacter);
        CharacterController controller = CharacterSelector.CurrentSelectedCharacter.GetComponent<CharacterController>();
        controller.radius = 0.2f;
    }


    public static IEnumerator playerPowerUp()
    {
        powerUpAnimFinished = true;

        CutsceneEnter.CurrentForwardSpeed = PlayerController.forwardSpeed;
        PlayerController.forwardSpeed = 0;
        PlayerController.maxSpeed = 0;
        CutsceneEnter.PowerApplies = true;
        CutsceneEnter.MainCamera_Reference.enabled = false;
        CutsceneEnter.cutsceneCam_Reference.enabled = true;
        PlayerController.animator.SetBool("Growing", true);
        CutsceneEnter.child_Animator_Reference.SetBool("PowerUp", true);

        while(powerUpAnimation == false)
        {
            //print("bruh");
           // print("bruh");
            yield return new WaitForSeconds(.01f);
        }
        powerUpAnimFinished = false;
       
        PlayerController.animator.SetBool("Growing", false);
        PlayerController.forwardSpeed = CutsceneEnter.CurrentForwardSpeed;   
        CutsceneEnter.powerupVar_PlayerController = false;

        yield return new WaitForSeconds(10);
        CutsceneEnter.child_Animator_Reference.SetBool("PowerUp", false);
        PlayerController.animator.SetBool("Shrinking", true);
        CutsceneEnter.child_Animator_Reference.SetBool("PowerDeactivate", true);

        CutsceneEnter.CurrentForwardSpeed = PlayerController.forwardSpeed;
        PlayerController.forwardSpeed = 0;
        PlayerController.maxSpeed = 0;
        
        yield return new WaitForSeconds(2.5f);
        powerUpAnimation = false;
        PlayerController.animator.SetBool("Shrinking", false);
        CutsceneEnter.child_Animator_Reference.SetBool("PowerDeactivate", false);
        CutsceneEnter.cutsceneCam_Reference.enabled = false;
        
        PlayerController.maxSpeed = 75;
        CutsceneEnter.powerupVar_PlayerController = false;
        PlayerController.forwardSpeed = CutsceneEnter.CurrentForwardSpeed;

        CutsceneEnter.FirstPersonCamera_Reference.enabled = false;
        CutsceneEnter.MainCamera_Reference.enabled = true;
        CutsceneEnter.PowerApplies = false;
        CutsceneEnter.powerUpChecker = false;
        powerUp_In_Progress = null;
        print("MADE IT TO THE END OF THE PLAYER POWER UP FUCNTION");
    }

   
    public static void ScaleCharacterModel()
    {
        PlayerController.child_Obj_Reference.transform.localScale = new Vector3(.5f, .5f, .5f);
        PlayerController.animator.enabled = false;
       
        //PlayerController.animator.enabled = true;
    }


    public  IEnumerator scaleUp()
    {
        IncreasePlayerCharacterController();
        float sizeGoal = 5;
        float starting = 1;
        while(starting < sizeGoal)
        {
            starting += .25f;
            PlayerController.PlayerModel.transform.localScale = new Vector3(starting, starting, starting);
            yield return new WaitForSeconds(.1f);
        }
    }

    public IEnumerator scaleDown()
    {
      
        float sizeGoal = 1;
        float starting = 5;
        while (starting > sizeGoal)
        {
            starting -= .25f;
            PlayerController.PlayerModel.transform.localScale = new Vector3(starting, starting, starting);
            yield return new WaitForSeconds(.1f);
        }
        DecreasePlayerCharacterController();
    }

     //HELPER FUNCTIONS 

     public static IEnumerator timer(int amountOfSeconds)
    {
        print("RUNRUNRUN");
        yield return new WaitForSeconds(amountOfSeconds);
    }


    public void LaneChange_Left()
    {
        
            PlayerController.animator.SetBool("LaneLeft", false);

    }



    public void LaneChange_Right()
    {
       
            PlayerController.animator.SetBool("LaneRight", false);
        
    }



    public void comboStringChange_1()
    {
        comboRatingNumber = 1;
               comboRating = "BASED!";
        
       
    }

    public void comboStringChange_2()
    {
        comboRatingNumber = 2;
        comboRating = "GREAT!";
        
    }

    public void comboStringChange_3()
    {
        comboRatingNumber = 3;
        comboRating = "NICE!";
        
    }


    public void comboStringChange_4()
    {
        comboRatingNumber = 4;
        comboRating = "Meh...";
        
    }


  
}
