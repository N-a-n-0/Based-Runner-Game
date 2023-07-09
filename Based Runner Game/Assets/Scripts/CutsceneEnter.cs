using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter : MonoBehaviour
{

    public static bool powerUpChecker = false;

    // public Animator animator;

   // public CameraController script;

  
   public   Camera MainCamera;
    public  GameObject cutsceneCam;



    // public string forwardspeedString = PlayerController.forwardSpeed;


    public float CurrentForwardSpeed = 25;

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(4);
        cutsceneCam.SetActive(false);
        // MainCamera.enabled = true;
        //  cutsceneCam.enabled = false;
        PlayerController.forwardSpeed = CurrentForwardSpeed;
        PlayerController.maxSpeed = 100;
        powerUpChecker = false;


    }
    



    void Update()
        {
       // print(powerUpChecker);
        if(powerUpChecker == true)
        {
            powerUpChecker = false;
            cutsceneCam.SetActive(true);
            CurrentForwardSpeed = PlayerController.forwardSpeed;
            PlayerController.forwardSpeed = 0;
            PlayerController.maxSpeed = 0;

           

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
