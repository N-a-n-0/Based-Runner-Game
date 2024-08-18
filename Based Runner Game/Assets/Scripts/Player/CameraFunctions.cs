using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    
    public static bool MicroGameEndReached = false;
    public static bool MicroGameStartReached = false;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void powerUpAnimationTrue()
    {
        print("ANIMATION CHECKER FINISHED WENT OFF");
        PlayerFunctions.powerUpAnimation = true;
    }

     
    public void MicroGameAnimStart()
    {
        print("Start of animation");
        MicroGameStartReached = true;
       
    }

    public void MicroStartFalse() //instead of using a start anim function for the camera I should use a function in usefulfunctions scripts for the text animations
    {
        MicroGameStartReached = false;
    }
  
    public void SetPositionToMainCamera()
    {
        this.gameObject.transform.position = CutsceneEnter.MainCamera_Reference.transform.position;
    }
    

    public void MicroGameAnimEnded()
    {
        MicroGameEndReached = true;
    }



}
