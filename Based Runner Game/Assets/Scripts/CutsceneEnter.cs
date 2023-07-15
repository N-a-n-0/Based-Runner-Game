using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter : MonoBehaviour
{

    public static bool powerUpChecker = false;
    public static bool powerupVar_PlayerController = false;

    [SerializeField]
   // public  bool playerObj;

    // public Animator animator;

    // public CameraController script;



    public   Camera MainCamera;
    public  GameObject cutsceneCam;



    // public string forwardspeedString = PlayerController.forwardSpeed;


    public static float CurrentForwardSpeed = 25;

    IEnumerator FinishCut()
    {
        print("ITS ABOUT TO WAIT");
        yield return new WaitForSeconds(4);
        PlayerController.forwardSpeed = CurrentForwardSpeed;
        cutsceneCam.SetActive(false);
        // MainCamera.enabled = true;
        //  cutsceneCam.enabled = false;
        
        PlayerController.maxSpeed = 75;


        print(PlayerController.forwardSpeed + "DA SPEED");

        powerUpChecker = false;
        powerupVar_PlayerController = false;
        print("IT MADE IT");
        StopAllCoroutines();
    }
    




    void Update()
        {
       // print(powerUpChecker);

        

        if(powerUpChecker == true)
        {


            cutsceneCam.SetActive(true);

            // MainCamera.enabled = false;
            // cutsceneCam.enabled = true;
            print("TRIGGERED");

            //  this.gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(FinishCut());
           
            //  animator.Play("StateName");
        }
        print(PlayerController.forwardSpeed);
     //   print(script.Camera.mainCamera);
    }
}
