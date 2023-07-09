using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObtained : MonoBehaviour
{
  //  public float CurrentForwardSpeed = PlayerController.forwardSpeed;


    void OnTriggerEnter(Collider other)
    {
        print(CutsceneEnter.powerUpChecker);
        CutsceneEnter.powerUpChecker = true;
        print(CutsceneEnter.powerUpChecker);
       // CurrentForwardSpeed = PlayerController.forwardSpeed;


        //  PlayerController.forwardSpeed = 0;
        //  PlayerController.maxSpeed = 0;
        // MainCamera.enabled = false;
        // cutsceneCam.enabled = true;
        //    print("TRIGGERED");

           this.gameObject.GetComponent<BoxCollider>().enabled = false;

        //  StartCoroutine(FinishCut());
        //  animator.Play("StateName");
    }
}
