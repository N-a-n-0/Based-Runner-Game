using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{

  //  public static bool comboHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        print("COMBO TRIGGERED");
        if((PlayerController.isSliding == true || PlayerController.isGrounded == false))
        {
            // comboHit = true;
            stylePoints();
            PlayerFunctions.comboText_Reference.text = PlayerFunctions.comboRating;
        }
    }

    public void stylePoints()
    {

        switch(PlayerFunctions.comboRatingNumber)
        {
            case 1:
                Score.currentScore += 1000;
                print("BASED you got the highest style rating");
                break;
            case 2:
                Score.currentScore += 750;
                print("GREAT you got the 2nd highest style rating");
                break;
            case 3:
                Score.currentScore += 500;
                print("NICE you got the 3rd highest style rating");
                break;
            case 4:
                Score.currentScore += 250;
                print("LAME you got the Lowest style rating");
                break;
            default:
                print("Wow it seems like you suck so much you dont get a style rating sorry :)");
                break;
        }

        
    }
}

