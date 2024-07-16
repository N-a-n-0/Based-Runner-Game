using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    
    public static bool gameOver;
 

    //public static bool Final_GameOver_Check;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;

    public Text coinsText;

    public static bool GameOverCutscene;

   

    void Start()
    {
        gameOver = false;
     
        isGameStarted = false;
        numberOfCoins = 0;
     //   Final_GameOver_Check = false;
    }

 
    void Update()
    {
       

        coinsText.text = "Coins: " + numberOfCoins;
        
        if (SwipeManager.tap)
        {
            isGameStarted=true;
            Destroy(startingText);
        }
    }
}
