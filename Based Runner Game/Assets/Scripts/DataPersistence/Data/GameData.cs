using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class GameData 
{
    

    public int coins;
    public int current_Level;
    

    public SerializableDictionary<int, bool> charactersUnlocked;

    public SerializableDictionary<int, bool> Extras_;
    public SerializableDictionary<int, bool> Levels_Unlocked;
    public SerializableDictionary<int, int> Level_HighScores;


    public GameData()
    {
        this.coins = 0;

       

        charactersUnlocked = new SerializableDictionary<int, bool>();

        Extras_ = new SerializableDictionary<int, bool>();

        Levels_Unlocked = new SerializableDictionary<int, bool>();

        Level_HighScores = new SerializableDictionary<int, int>();
    }
}
