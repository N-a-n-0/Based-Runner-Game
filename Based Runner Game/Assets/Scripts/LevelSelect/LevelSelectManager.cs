using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//LEVEL SELECT WILL USE SUPER MARIO BROTHERS WII AS THE FOUNDATION AS HOW THE SYSTEM WILL BE DESIGNED 
public class LevelSelectManager : MonoBehaviour, IDataPersistence
{
   

    public GameObject characterModel;

    public Button Next_Level_Button;
    public Button Pervious_Level_Button;

    public LevelBluePrint[] Level_info;

    public GameObject[] Level_Locations;

    public TMP_Text level_Name_UI;
    public TMP_Text level_High_Score_UI ;


    // public List<Transform> positions;

    public int currentLevel = 0;

    



    public void LoadData(GameData data)
    {
     

        for (int i = 0; i < Level_Locations.Length; i++)
        {
          //  bool bruh = true;
            print("BOOL DIC VALUE ");
         //   print(bruh);
           // print(data.Levels_Unlocked.TryGetValue(i, out bruh));
            data.Levels_Unlocked.TryGetValue(i, out Level_info[i].isUnlocked);
         //   print(bruh);
            data.Level_HighScores.TryGetValue(i, out Level_info[i].level_HighScore);
            if (Level_info[i].isUnlocked)
            {
            //    print(Level_info[i].isUnlocked);
            }

        
              //  print(Level_info[i].level_HighScore);

            
    //        data.Level_Locations.TryGetValue(i, out //NEED TO MOST LIKLEY MAKE A NEW CLASS TO STORE DATA ABOUT THE HIGHSCHOOL VALUE )
        }
      
    }
    public void SaveData(GameData data)
    {
        //level_HighScore is our local variable
        //Level_HighScores is our SerializableDictionary with our saved values when the game shuts down

        //isUnlocked is our local variable
        //Levels_Unlocked is our SerializableDictionary with our saved values when the game shuts down

        for (int i = 0; i < Level_Locations.Length; i++)
         {
         //   int brub = 500;
          //  data.Level_HighScores.TryGetValue(5, out brub);
            if (data.Levels_Unlocked.ContainsKey(i))
             {
                 data.Levels_Unlocked.Remove(i);
             }
             data.Levels_Unlocked.Add(i, Level_info[i].isUnlocked);



            if (data.Level_HighScores.ContainsKey(i))
            {
                data.Level_HighScores.Remove(i);
              
            }
            data.Level_HighScores.Add(i, Level_info[i].level_HighScore);


            if(Level_info[i].level_HighScore > 1)
            {

            }

          

            print(data.Level_HighScores.ContainsKey(i));
        }
        
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
            level_Name_UI.text = "Level Name: " + Level_info[currentLevel].level_Name;
            level_High_Score_UI.text = "Level HISCORE: " + Level_info[currentLevel].level_HighScore;
            Pervious_Level_Button.enabled = true;
            Next_Level_Button.enabled = true;
            // print("POINT HAS BEEN REACHED");

        }
        else
        {
            Pervious_Level_Button.enabled = false;
            Next_Level_Button.enabled = false;
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
