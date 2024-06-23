using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

    }


    public void powerUpAnimationTrue()
    {
        print("ANIMATION CHECKER FINISHED WENT OFF");
        PlayerFunctions.powerUpAnimation = true;
    }
}
