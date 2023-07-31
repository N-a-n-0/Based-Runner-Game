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



    public void PlayGame()
    {
        DisablePlayButton();
     //   DataPersistenceManager.instance.NewGame();
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

    private void DisablePlayButton()
    {
        StartGameButton.interactable = false;
    }

    private void DisableShopButton()
    {
        ShopButton.interactable = false;
    }
}
