using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IDataPersistence
{
    public static int coinsCollected;
    public float ScaleValueIncrease = 1f;
    public IEnumerator timercheck = null;

    public GameObject coinModel;
    // Start is called before the first frame update
    void Start()
    {
        ScaleValueIncrease = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(80 * Time.deltaTime, 0, 0);
    }
    
    public void LoadData(GameData data)
    {
        coinsCollected = data.coins;
        
    }
    public void SaveData(GameData data)
    {
        data.coins = coinsCollected;

    }

    public int getCoins()
        { return coinsCollected; }


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

    private void OnTriggerEnter(Collider other)
    {
        
            


        print("COINS COLLECTED" + coinsCollected);
        if(other.tag == "Player")
        {

            if(CutsceneEnter.powerUpChecker == false)
            {
                ProgressBar.targetProgress += 0.10f;
            }
           

            if (ProgressBar.targetProgress >= 1)
            {
                ProgressBar.targetProgress = 0;
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
            }
          //  print("THIS RAN");
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            coinsCollected++;
           
            PlayerManager.numberOfCoins += 1;
            //PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0) + 1);
            // Destroy(gameObject);

            //  Debug.Log(coinsCollected);

            // Destroy(gameObject);
            Destroy(coinModel);
        }
    }
}
