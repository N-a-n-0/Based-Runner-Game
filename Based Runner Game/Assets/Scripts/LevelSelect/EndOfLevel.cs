using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

public class EndOfLevel : MonoBehaviour
{
    // public LevelSelectManager obj;
    public static bool gameWasBeaten;

    public Animator animator;

    public Camera winCam;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("GoalCamera").GetComponent<Animator>();
        winCam = GameObject.Find("GoalCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        gameWasBeaten = true;
        CutsceneEnter.MainCamera_Reference.enabled = false;
        
        PlayerController.NonPlayerableButtManModelRef.SetActive(true);

    //    winCam.transform.position = CutsceneEnter.MainCamera_Reference.transform.position;
    //  winCam.transform.rotation = CutsceneEnter.MainCamera_Reference.transform.rotation;
     //  winCam.transform.localScale = CutsceneEnter.MainCamera_Reference.transform.localScale;
        animator.enabled = true;
        winCam.enabled = true;
        print("THIS FOR SURE RAN BRO");
        //CutsceneEnter.powerupVar_PlayerController = true;

        LevelSelectManager.Level_info_Reference[LevelSelectManager.currentLevel + 1].isUnlocked = true;

        if(Score.currentScore > LevelSelectManager.Level_info_Reference[LevelSelectManager.currentLevel].level_HighScore) //If the current score beats the current high score replace it
        {
            LevelSelectManager.Level_info_Reference[LevelSelectManager.currentLevel].level_HighScore = Mathf.Round(Score.currentScore);  //OVERWRITING HIGHSCORE!!!
        }
       

    }

}