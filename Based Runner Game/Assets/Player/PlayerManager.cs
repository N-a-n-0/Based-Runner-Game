using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameOver;
    public GameObject gameOverPanel;



    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;

    public Text coinsText;

   // public GameData data;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
    }
   

    // Update is called once per frame
    void Update()
    {
       // print(Coin.coinsCollected);
        if(gameOver)
        {
            PlayerController.forwardSpeed = 25;
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        coinsText.text = "Coins: " + numberOfCoins;
        
        if (SwipeManager.tap)
        {
            isGameStarted=true;
            Destroy(startingText);
        }
    }
}
