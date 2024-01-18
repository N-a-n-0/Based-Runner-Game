using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//LEVEL SELECT WILL USE SUPER MARIO BROTHERS WII AS THE FOUNDATION AS HOW THE SYSTEM WILL BE DESIGNED 
public class LevelSelectManager : MonoBehaviour, IDataPersistence
{


    public GameObject characterModel;

    public Button Next_Level_Button;
    public Button Pervious_Level_Button;
    public Button Enter_Level_Button;

    public LevelBluePrint[] Level_info;

    public static LevelBluePrint[] Level_info_Reference;

    public GameObject[] Level_Locations;

    public TMP_Text level_Name_UI;
    public TMP_Text level_High_Score_UI;


    // public List<Transform> positions;

    public int currentLevel;

    

    //DONT FORGET YOU PROLLY NEED TO CREATE A NEW VALUE FOR YOUR JSON FILE IN ORDER TO KEEP TRACK OF WHICH LEVEL THE PLAYER ENTERS AND IS CURRENTLY AT IN THE CODE

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


            if (Level_info[i].level_HighScore > 1)
            {

            }



            print(data.Level_HighScores.ContainsKey(i));
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        Level_info_Reference = Level_info;


        for (int i = 0; i < Level_Locations.Length; i++) { Print_Level_Info(i); }

    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level_Select")
        {




            if (characterModel.transform.position == Level_Locations[currentLevel].transform.position)
            {
                level_Name_UI.text = "Level Name: " + Level_info[currentLevel].level_Name;
                level_High_Score_UI.text = "Level HISCORE: " + Level_info[currentLevel].level_HighScore;
                Pervious_Level_Button.enabled = true;
                Next_Level_Button.enabled = true;
                Enter_Level_Button.enabled = true;
                // print("POINT HAS BEEN REACHED");

            }

            else
            {
                Pervious_Level_Button.enabled = false;
                Next_Level_Button.enabled = false;
                Enter_Level_Button.enabled = false;
                characterModel.transform.position = Vector3.MoveTowards(characterModel.transform.position, Level_Locations[currentLevel].transform.position, 12 * Time.deltaTime);
            }

        }
    }



    public void Move_To_Level()
    {
        if (currentLevel + 1 > Level_Locations.Length - 1)
        {
            print("You have reached the end of the array");
        }
        else if (Level_info[currentLevel + 1].isUnlocked == false)
        {
            print("The next level has not been unlocked yet please play the current level to unlock it");
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

        else if (Level_info[currentLevel - 1].isUnlocked == false)
        {
            print("The next level has not been unlocked yet please play the current level to unlock it");
        }
        else
        {
            currentLevel--;
        }

    }


    public void Enter_Current_Level()
    {

    }

    public void Print_Level_Info(int level_Number)
    {
        print("Name: " + Level_info_Reference[level_Number].level_Name + "\n" +
        "High Score: " + Level_info_Reference[level_Number].level_HighScore + "\n" +
        "Unlocked Status: " + Level_info_Reference[level_Number].isUnlocked + "\n" +
        "Description: " + Level_info_Reference[level_Number].description + "\n");
    }





    

}
