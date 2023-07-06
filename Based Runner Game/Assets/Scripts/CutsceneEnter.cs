using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutsceneCam;
   // public string forwardspeedString = PlayerController.forwardSpeed;
    public float CurrentForwardSpeed = PlayerController.forwardSpeed;
    void OnTriggerEnter(Collider other)
    {
         CurrentForwardSpeed = PlayerController.forwardSpeed;


    PlayerController.forwardSpeed = 0;
    PlayerController.maxSpeed = 0;

        print("TRIGGERED");

        this.gameObject.GetComponent<BoxCollider>().enabled = false;
       // cutsceneCam.SetActive(true);
        //thePlayer.SetActive(false);
        StartCoroutine(FinishCut());
       
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(5);
        PlayerController.forwardSpeed = CurrentForwardSpeed;
        PlayerController.maxSpeed = 100;
        // thePlayer.SetActive(true);
        //  cutsceneCam.SetActive(false);
    }

     void Update()
        {
        print(PlayerController.forwardSpeed);
        }
}
