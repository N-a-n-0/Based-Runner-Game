using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{

    [Header("Menu Buttons")]
    [SerializeField] private Button StartGameButton;
    [SerializeField] private Button ShopButton;

    private void Start()
    {
        
    }


    public void PlayGame()
    {
        DisablePlayButton();
       // if (DataPersistenceManager.instance.HasGameData())
        //    {
          //  Debug.Log("NO DATA? NEW GAME THEN!!!!!");
         //   DataPersistenceManager.instance.NewGame();
         //   }
      
        SceneManager.LoadSceneAsync("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToShop()
    {
        DisableShopButton();

        SceneManager.LoadSceneAsync("Shop");
    }


    public void GoToLevels()
    {
       // DisableShopButton();

        SceneManager.LoadSceneAsync("Level_Select");
    }

    private void DisablePlayButton()
    {
        StartGameButton.interactable = false;
    }

    private void DisableShopButton()
    {
        ShopButton.interactable = false;
    }
}
