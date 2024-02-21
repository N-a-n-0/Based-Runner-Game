using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class EndOfLevel : MonoBehaviour
{
  // public LevelSelectManager obj;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        print("THIS FOR SURE RAN BRO");
        LevelSelectManager.Level_info_Reference[LevelSelectManager.currentLevel + 1].isUnlocked = true;

        if(Score.currentScore > LevelSelectManager.Level_info_Reference[LevelSelectManager.currentLevel].level_HighScore) //If the current score beats the current high score replace it
        {
            LevelSelectManager.Level_info_Reference[LevelSelectManager.currentLevel].level_HighScore = Mathf.Round(Score.currentScore);  //OVERWRITING HIGHSCORE!!!
        }
       

    }




}