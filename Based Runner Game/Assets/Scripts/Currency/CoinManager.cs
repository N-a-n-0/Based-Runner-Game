using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CoinManager : MonoBehaviour, IDataPersistence
{
    public static  int coinsCollected;
//    public int coin;
    

   

    public void LoadData(GameData data)
    {
        
        coinsCollected = data.coins;
        print("COINS COLLECTED IN LOAD DATA: " + coinsCollected);

      

    }
    public void SaveData(GameData data)
    {
       
        data.coins = coinsCollected;
        print("CURRENT SAVED DATA FOR COINS " + data.coins);
      
        // DataPersistenceManager.SaveGame();
    }

    public int returnCoins()
    {
        return coinsCollected;
    }

    public void Start()
    {
        
    }

}
