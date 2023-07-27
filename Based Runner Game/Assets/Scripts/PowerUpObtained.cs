using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObtained : MonoBehaviour
{
    //  public float CurrentForwardSpeed = PlayerController.forwardSpeed;

    public IEnumerator timercheck = null;

    public float ScaleValueIncrease = 1f;
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

    void OnTriggerEnter(Collider other)
    {
        print("BOX WAS HIT NOW TURNING OFF BOX COLLIDER");
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        timercheck = Waiter();

        print(CutsceneEnter.powerUpChecker);
       
        CutsceneEnter.powerUpChecker = true;
        print(CutsceneEnter.powerUpChecker);



       // CutsceneEnter.powerUpChecker = true;
        CutsceneEnter.powerupVar_PlayerController = true;
       
        CutsceneEnter.CurrentForwardSpeed = PlayerController.forwardSpeed;
        PlayerController.forwardSpeed = 0;
        PlayerController.maxSpeed = 0;



        StartCoroutine(timercheck);
        



        // CurrentForwardSpeed = PlayerController.forwardSpeed;



        //  PlayerController.forwardSpeed = 0;
        //  PlayerController.maxSpeed = 0;
        // MainCamera.enabled = false;
        // cutsceneCam.enabled = true;
        //    print("TRIGGERED");



        //  StartCoroutine(FinishCut());
        //  animator.Play("StateName");
    }
}
