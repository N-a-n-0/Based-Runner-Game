using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class GameData 
{
    

    public int coins;

    

    public SerializableDictionary<int, bool> charactersUnlocked;

    public SerializableDictionary<int, bool> Extras_;

   

    public GameData()
    {
       

       

        this.coins = 0;

       

        charactersUnlocked = new SerializableDictionary<int, bool>();

        Extras_ = new SerializableDictionary<int, bool>();
    }
}
