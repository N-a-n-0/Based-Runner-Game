using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObtained : MonoBehaviour
{
    public IEnumerator timercheck = null;

    public float ScaleValueIncrease = 1f;
    //Waiter increases the scale of PlayerModel
    IEnumerator Waiter()
    {

        PlayerController.PlayerModel.transform.position += new Vector3(0, 3.5f, 0);
        while (ScaleValueIncrease > 6f == false)
        {
            yield return new WaitForSeconds(.1f);
            ScaleValueIncrease += .170f;

            if (ScaleValueIncrease > 6f)
            {
                ScaleValueIncrease = 6f;
            }
            PlayerController.PlayerModel.transform.localScale = new Vector3(ScaleValueIncrease, ScaleValueIncrease, ScaleValueIncrease);
        }
        timercheck = null;
        StopAllCoroutines();
    }
    //Power up is attached to the PowerUp prefab
    void powerUpLogic()
    {

        CutsceneEnter.powerUpChecker = true;
             ProgressBar.targetProgress = 0;
      
    }

    void OnTriggerEnter(Collider other)
    {

        if (CutsceneEnter.powerUpChecker == false)
        {
          
                powerUpLogic(); 

        }
        else
        {
            print("POWER UP IS ALREADY IN PROGRESS RIGHT HERE SHOULD ADD TO THE PLAYER OVERALL SCORE IF POWER UP WAS OBTAINED AGAIN");
        }

       
    }
}
