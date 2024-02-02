using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour//, IDataPersistence
{
  
    public float ScaleValueIncrease = 1f;
    public IEnumerator timercheck = null;

    public GameObject coinModel;

    
    void Start()
    {
      //  Time.timeScale = 5;
       ScaleValueIncrease = 1f;
      //  print(CoinManager.coinsCollected);
    }

    
    void Update()
    {
        transform.Rotate(80 * Time.deltaTime, 0, 0);
      //  print(CoinManager.coinsCollected);
    }
    
    

    public int getCoins()
        { return CoinManager.coinsCollected; }


   // IEnumerator Waiter()
   // {
       /* PlayerController.PlayerModel.transform.position += new Vector3(0, 3.5f, 0);
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


        */
   // }

    void coinPowerUpLogic(Collider other)
    {
      
        if (other.tag == "Player")
        {
            CoinManager.coinsCollected ++;
            PlayerManager.numberOfCoins = CoinManager.coinsCollected;
            print("COINS COLLECTED" + CoinManager.coinsCollected);
            if (ProgressBar.targetProgress < 1 && CutsceneEnter.powerUpChecker == false)
            {
                ProgressBar.targetProgress += .01f;
            }


            if (ProgressBar.targetProgress >= 1 && CutsceneEnter.powerUpChecker == false)
            {
                ProgressBar.targetProgress = 0;
                CutsceneEnter.powerUpChecker = true;
               


                /*
                  timercheck = Waiter();

                  print(CutsceneEnter.powerUpChecker);

                  CutsceneEnter.powerUpChecker = true;
                  print(CutsceneEnter.powerUpChecker);




                  CutsceneEnter.powerupVar_PlayerController = true;

                  CutsceneEnter.CurrentForwardSpeed = PlayerController.forwardSpeed;
                  PlayerController.forwardSpeed = 0;
                  PlayerController.maxSpeed = 0;



                  StartCoroutine(timercheck);
                */
            }
            else
            {
                print("Conditions for power up has been met while power up has already been activated");
            }

            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
           

            Destroy(coinModel);
        }
    }

    private void OnTriggerEnter(Collider other)
    {



        coinPowerUpLogic(other);


    }
}
