using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class GameData 
{
    

    public int coins;

    

    public SerializableDictionary<int, bool> charactersUnlocked;

  //  public int playerSelected;

    public GameData()
    {
        //this.deathCount = 0;

       

        this.coins = 0;

       // this.playerSelected = 0;

        charactersUnlocked = new SerializableDictionary<int, bool>();
    }
}
