using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;


//LEVEL SELECT WILL USE SUPER MARIO BROTHERS WII AS THE FOUNDATION AS HOW THE SYSTEM WILL BE DESIGNED 
public class LevelSelectManager : MonoBehaviour, IDataPersistence
{

    public GameObject characterModel;

    public GameObject[] Level_Locations;
    public int[] Level_Location_HighScores;

    public List<Transform> positions;

    public int currentLevel = 0;

    



    public void LoadData(GameData data)
    {

      //  data.Levels_.Add(0, data.coins);
      /*  for (int i = 0; i < characters.Length; i++)
        {
            data.charactersUnlocked.TryGetValue(i, out characters[i].isUnlocked);

            if (characters[i].isUnlocked)
            {

            }


        }*/
    }
    public void SaveData(GameData data)
    {
        //  data.coins = CoinManager.coinsCollected;
        //  data.Levels_.Add(2, 1235231);
        //data.Levels_.Remove(2);


        /* for (int i = 0; i < characters.Length; i++)
         {

             if (data.charactersUnlocked.ContainsKey(i))
             {
                 data.charactersUnlocked.Remove(i);
             }
             data.charactersUnlocked.Add(i, characters[i].isUnlocked);
         }*/
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      
        if (characterModel.transform.position == Level_Locations[currentLevel].transform.position)
        {

           // print("POINT HAS BEEN REACHED");

        }
        else
        {
            characterModel.transform.position = Vector3.MoveTowards(characterModel.transform.position, Level_Locations[currentLevel].transform.position, 12 * Time.deltaTime);
        }

        
    }



    public void Move_To_Level()
    {
        if(currentLevel + 1 > Level_Locations.Length - 1) 
        {
            print("You have reached the end of the array");
        }
        else
        {
            currentLevel++;
        }
      
    }


    public void Move_To_Previous_Level()
    {
        if (currentLevel - 1 < 0)
        {
            print("You have reached the end of the array");
        }
        else
        {
            currentLevel--;
        }

    }
}
