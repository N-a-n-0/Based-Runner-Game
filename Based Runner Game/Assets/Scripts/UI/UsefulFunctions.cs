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
}
