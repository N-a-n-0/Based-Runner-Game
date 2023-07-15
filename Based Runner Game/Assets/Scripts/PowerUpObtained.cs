using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObtained : MonoBehaviour
{
  //  public float CurrentForwardSpeed = PlayerController.forwardSpeed;


    void OnTriggerEnter(Collider other)
    {
        print("BOX WAS HIT NOW TURNING OFF BOX COLLIDER");
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        print(CutsceneEnter.powerUpChecker);
       
        CutsceneEnter.powerUpChecker = true;
        print(CutsceneEnter.powerUpChecker);

        CutsceneEnter.powerUpChecker = true;
        CutsceneEnter.powerupVar_PlayerController = true;
       
        CutsceneEnter.CurrentForwardSpeed = PlayerController.forwardSpeed;
        PlayerController.forwardSpeed = 0;
        PlayerController.maxSpeed = 0;


        // CurrentForwardSpeed = PlayerController.forwardSpeed;



        //  PlayerController.forwardSpeed = 0;
        //  PlayerController.maxSpeed = 0;
        // MainCamera.enabled = false;
        // cutsceneCam.enabled = true;
        //    print("TRIGGERED");



        //  StartCoroutine(FinishCut());
        //  animator.Play("StateName");
    }
}
