using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    
    public static bool MicroGameEndReached = false;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void powerUpAnimationTrue()
    {
        print("ANIMATION CHECKER FINISHED WENT OFF");
        PlayerFunctions.powerUpAnimation = true;
    }


    public   void MicroMiniGameTimer()
    {
        MicroGameEndReached = true;
       // while (MicroGameEndReached == false)
      //  {
      //      yield return new WaitForSeconds(.1f);
      //  }


       
      
    }

    public void MicroGameAnimEnded()
    {
        MicroGameEndReached = true;
    }

}
