using System.Collections;
using System.Collections.Generic;
 
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

using UnityEngine.UI;

using TMPro;
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

    public static MicroMath MiniG_1;

    public static  RaycastMinigames MiniG_2;

    public static IEnumerator ActiveCoroutine = null;

   // public MethodInfo method = null;

    public static bool MiniGameComplete;

    public static bool timeRanOut;

    public static int forloopIndex;

    public static int selectedMicroGame = -1;

    public TMP_Text UniversalObjectiveText; //we shall use this for any microgame as the objective text when given the opportunity (otherwise you can make a custom one like we have for MicroMath if needed)

    public Animator UniversalObjectiveTextAnim;  
    public static Animator UniversalObjectiveTextAnimReference; //anim ref

    public GameObject UniversalObjectiveTextObject;
    public static GameObject UniversalObjectiveTextObjectReference;

    public static string ObjectiveText = " "; //static so we can use it across many different scripts

    public static bool AnswerPicked = false;

    [SerializeField] private AnimatorOverrideController[] overrideControllers;
   // [SerializeField] private AnimatorOverrider overrider;

    void Start()
    {
       
        print(this); 
        UniversalObjectiveTextAnimReference = UniversalObjectiveTextAnim;
        UniversalObjectiveTextObjectReference = UniversalObjectiveTextObject;
     //  UniversalObjectiveTextAnim.runtimeAnimatorController = overrideControllers[0]; //this changes the animations of an animator
    }
    

    void Update()
    {

        if(ObjectiveText != " ")
        {
            UniversalObjectiveText.text = ObjectiveText;

        }
        
        if (alreadyEntered == true && CameraFunctions.MicroGameEndReached == true && ActiveCoroutine == null && selectedMicroGame > -1)
        {

            
            switch (selectedMicroGame)
            {
                case 0:
                    UniversalObjectiveTextAnim.runtimeAnimatorController = overrideControllers[0];
                    ActiveCoroutine = MathMicrogame_0();
                    print("function " + selectedMicroGame + " found");
                    break;
                case 1:
                    UniversalObjectiveTextAnim.runtimeAnimatorController = overrideControllers[1];
                    ActiveCoroutine = TouchMicrogame_1();
                    print("function " + selectedMicroGame + " found");
                    break;
                case 2:
                    
                    print("function " + selectedMicroGame + " found");
                    break;
                case 3:
                    
                    print("function " + selectedMicroGame + " found");
                    break;
                default:
                    print("No Function found");
                    break;
            }
//            ActiveCoroutine = MathMicrogame_0(); //maybe make the 0 part of the function name be some kind of int varaible to make it easier to call Ienumerators EXAMPLE: "MathMicrogame_" + MiniGame_ID rather than making a new if statement 


            print(ActiveCoroutine);
            StartCoroutine(ActiveCoroutine);
           
        }
    }

  
    private IEnumerator MathMicrogame_0()
    {
        ObjectiveText = "SOLVE IT";
        UniversalObjectiveTextObjectReference.SetActive(true);
        savedSpeed = curSpeed;


        CutsceneEnter.MainCamera_Reference.enabled = false;
        microGameTemplates[0].MicroGameObj.SetActive(true);
         
       
       VisibleTimer = microGameTemplates[0].miniGameTime * 10;
      

        //need to figure out a way to cutoff the MicroGame once the player gets the correct answer. Maybe another animation? Maybe use code from MicroMath?
        
        for (forloopIndex = VisibleTimer ; forloopIndex > 0; forloopIndex--) // this will probably need to be  awhile loop and we should run the timer off of something else we need to find a way to make sure the time doesnt overwrite if last sec
        {
            print(VisibleTimer + "Seconds left");
            VisibleTimer--;
            yield return new WaitForSeconds(.1f);
           
           
        }

        if (AnswerPicked == false)
        {
            MiniG_1.IncorrectPick();
        }

            while (MiniGameComplete == false || forloopIndex > 0)
        {
            print("waiting for animation to end" + MiniGameComplete + forloopIndex);
            yield return new WaitForSeconds(.1f);
        }
        UniversalObjectiveTextObjectReference.SetActive(false);
        // MicroGameManager.UniversalObjectiveTextObjectReference.SetActive(false);
        MiniGameComplete = false;

        MiniG_1.RemoveListeners();
            MiniG_1.GenerateAnswer();

            MicroGame.MicroCamRef.enabled = false;
            CutsceneEnter.MainCamera_Reference.enabled = true;

            PlayerController.forwardSpeed = savedSpeed;
            CameraFunctions.MicroGameEndReached = false;

            microGameTemplates[0].MicroGameObj.SetActive(false);

        selectedMicroGame = -1;
            ActiveCoroutine = null;
            alreadyEntered = false;
       


    }

    private IEnumerator TouchMicrogame_1() //Look in RaycastMinigames in order to start scripting our game here
    {
      //  ObjectiveText = "Touch Red";
         UniversalObjectiveTextObjectReference.SetActive(true);
        //We may need to change how we handle text objective by setting the objective text right here at the start you should be able to just change the static string objective text

        savedSpeed = curSpeed;


        CutsceneEnter.MainCamera_Reference.enabled = false;
        VisibleTimer = microGameTemplates[1].miniGameTime * 10;

        for (forloopIndex = VisibleTimer; forloopIndex > 0; forloopIndex--) // this will probably need to be  awhile loop and we should run the timer off of something else we need to find a way to make sure the time doesnt overwrite if last sec
        {
            print(VisibleTimer + "Seconds left");
            VisibleTimer--;
            yield return new WaitForSeconds(.1f);
        }



        if (AnswerPicked == false) 
        {
            //  ObjectiveText = "Failure!!!";
            MiniG_2.Failure();

            // MiniG_1.IncorrectPick(); this commented code at the moment was used in the first minigame does not apply 
        }

        while (MiniGameComplete == false || forloopIndex > 0) //check until all conditions are meet
        {
            print("waiting for animation to end" + MiniGameComplete + forloopIndex);
            yield return new WaitForSeconds(.1f);
        }

        UniversalObjectiveTextObjectReference.SetActive(false);
        MiniGameComplete = false;

        MicroGame.MicroCamRef.enabled = false;
        CutsceneEnter.MainCamera_Reference.enabled = true;

        PlayerController.forwardSpeed = savedSpeed;
        CameraFunctions.MicroGameEndReached = false;
        
        AnswerPicked = false; //Reason why first function doesnt ahve this is because it is set to false in the Micromath Script I should try to make a script that I can call from anywhere to keep this consisent 

        selectedMicroGame = -1;
        ActiveCoroutine = null;
        alreadyEntered = false;
    }

    
}
