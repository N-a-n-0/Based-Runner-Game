using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool Final_GameOver_Check;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;

    public Text coinsText;

    public static bool GameOverCutscene;

    public IEnumerator GAMEOVER = null;

    // public GameData data;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
        Final_GameOver_Check = false;
    }

    public IEnumerator GameOverTimer()
    {
        Final_GameOver_Check = true;
        gameOver = false;
        PlayerController.forwardSpeed = 0;

        // 
        yield return null;
        //yield return new WaitForSeconds(2);
        


        gameOverPanel.SetActive(true);
        GameOverCutscene = true;
        Time.timeScale = 0;
        print("GAMEOVER_RAN");
      
       
        
      
    }

    // Update is called once per frame
    void Update()
    {
       // print(Coin.coinsCollected);
        if(gameOver == true && GAMEOVER == null)
        {
            GAMEOVER = GameOverTimer();
            StartCoroutine(GAMEOVER);

            print("GAMEOVER IS CURRENTLY TRUE ");
            
           

           

           
           if(GameOverCutscene == false)
            {
               
            }
           
        }

        coinsText.text = "Coins: " + numberOfCoins;
        
        if (SwipeManager.tap)
        {
            isGameStarted=true;
            Destroy(startingText);
        }
    }
}
