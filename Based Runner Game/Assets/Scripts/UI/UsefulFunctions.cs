using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulFunctions : MonoBehaviour
{
     public void forLoopIndexSetter()
    {
        MicroGameManager.forloopIndex = 0;
        MicroGameManager.VisibleTimer = 0;
    }

    public void AnimationEnd()
    {
        MicroGameManager.MiniGameComplete = true;
    }

    public void DisableMainPlayerRender()
    {
        PlayerController.buttmanGameObjRef.SetActive(false);
    }

    public void EnableLevelCompleteUI()
    {
        UIElements.LevelCompletePanel_Reference.SetActive(true);
    }

    //LEARN HOW TO PLAY ANIMATIONS DIRECTLY AND FIGURE OUT WHEN WE CAN APPLY THIS TO SIMPLIFIY THE PROCESS AND MAKE IT SO LIKE DASHING LEFT AND RIGHT CAN JUST BE PLAYED AND ONLY HAVE A ONE WAY TRANSITION IN THE ANIMATOR
}
