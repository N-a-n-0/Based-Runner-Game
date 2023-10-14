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

    public  bool Increasing = false;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        targetProgress = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetProgress(1f);
        // slider.value = 1f;
        Increasing = true;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = targetProgress;

        /*
        if(timeLeft > 0 && Decreasing == true)
        {
            Increasing=false;
          //  print("TIME IS GOING DOWN");
            timeLeft -= Time.deltaTime;
            slider.value = timeLeft / maxTime;
        }
      //  if(slider.value < targetProgress)
      //  {
       //     slider.value += FillSpeed * Time.deltaTime;
      //  }

        if(Increasing == true && slider.value < 1)
        {
          //  print("TIME IS GOING UP");
            timeLeft += Time.deltaTime;
            slider.value = timeLeft / maxTime;
            //  slider.value -= 0.1f * Time.deltaTime;
            // DecreaseToMin();

        }
        else
        {
            Decreasing = true;
        }
        */
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

       // while(Decreasing == true)
       // {
            

          //  if(slider.value <= 0)
          //  {
           //     Decreasing = false;
         //   }

        //}

    }

}
