using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    private Slider slider;



    public float maxTime;

    float timeLeft = 0;

    public static float targetProgress = 0;

    public  bool Decreasing = false;

   

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        targetProgress = 0;
    }

   
   
   
    void Update()
    {
        slider.value = targetProgress;

    }


    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    public void SetProgress(float newProgress)
    {
        targetProgress = newProgress;
    }

    public void DecreaseToMin()
    {
        Decreasing = true;
        targetProgress = 0;

      

    }

}
