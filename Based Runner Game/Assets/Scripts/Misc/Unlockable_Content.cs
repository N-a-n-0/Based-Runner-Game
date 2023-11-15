using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unlockable_Content : MonoBehaviour
{



    public Extras extras_Reference;

    public static bool ArtUnlocked = false;
    public static int randomNumber = 0; 
    

  
    void Update()
    {

        if(ArtUnlocked == true)
        {
            print("UNLOCKABLE NUMBER" + randomNumber);
            print("WE HAVE ENTERED THE IF STATEMENT");
            ArtUnlocked = false;
            Unlock_Art();
        }
        
    }


    
   public void Unlock_Art()
    {
       
     
            if (extras_Reference.extras[randomNumber].isUnlocked == false)
            {
            print("ENTERED IF STATEMENT");
            extras_Reference.extras[randomNumber].isUnlocked = true;
            }
            else
            {
            print("THIS PIECE OF EXTRA BONUS CONTENT HAS ALREADY BEEN UNLOCKED");
            }
            

        
    }
}
