using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Item_Trigger : MonoBehaviour
{

    
    public int NewRandomNumber()
    {
        return Random.Range(0, 6);

    }

    private void OnTriggerEnter(Collider other)
    {
        print("EXTRA CONTENT HIT BOX WAS HIT");
        if(Unlockable_Content.ArtUnlocked == false)
        {
            Unlockable_Content.ArtUnlocked = true;
            Unlockable_Content.randomNumber = NewRandomNumber();

         
        }
        else
        {
            Unlockable_Content.ArtUnlocked = false;
           
        }
        
    }
}
