using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unlockable_Content : MonoBehaviour
{



    public Extras extras_Reference;

    int randomNumber = 0;
    

    // Start is called before the first frame update
    void Start()
    {


      //  extras_Reference.extras[0].isUnlocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("EXTRA CONTENT HIT BOX WAS HIT");
        NewRandomNumber();
    }

    
   public void NewRandomNumber()
    {
       
        randomNumber = Random.Range(0, 6);

       // print(randomNumber);
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
