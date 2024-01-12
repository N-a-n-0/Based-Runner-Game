using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimation : MonoBehaviour
{

    public static bool PlayerHasFallen = false;
   
    public Animator animator;

    public GameObject gameOverPanel;

    public GameObject DeathModel;

    public GameObject ButtManModel;
   

    public void ButtManDeathMethod()
    {
        
        ButtManModel.SetActive(false);
        print("DEAHT ANIMATION FUNCITON HAS RAN");
        GameObject cloneDeath = Instantiate(DeathModel);
        cloneDeath.transform.parent = this.transform;
        cloneDeath.transform.position = this.transform.position;
        cloneDeath.transform.localScale = new Vector3(1,1,1);

    }

    public void CameraAnimatorEnabled()
    {
        animator.enabled = true;
    }

   

    public void GameOverMenu()
    {
        PlayerManager.Final_GameOver_Check = true;
       
        gameOverPanel.SetActive(true);
      
        Time.timeScale = 0;
        print("GAMEOVER_RAN");
    }

    public void Game_False()
    {
        PlayerManager.gameOver = false;
        PlayerController.animator.SetBool("Game Over", false);
    }

}
