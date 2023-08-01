using System.Collections;

using UnityEngine;


public class UIFade : MonoBehaviour
{

    private bool isFading = false;

    public CanvasGroup MainMenu;
    public CanvasGroup SettingsMenu;
    public CanvasGroup ExtrasMenu;

    public GameObject settingsObj;
    public GameObject extrasObj;

    IEnumerator FadeFromTo(float from, float to)
    {
        isFading = true;
        var curve = new AnimationCurve(new Keyframe[]
        {
            new Keyframe(0f, from),
            new Keyframe(1f, to)
        });

        float time = 0f;

        while(time < 1f)
        {
            MainMenu.alpha = curve.Evaluate(time);
            time += 0.5f * Time.deltaTime;
            yield return null;
        }


        MainMenu.alpha = curve.Evaluate(1f);

        isFading = false;
    }


    IEnumerator FadeFromToNewMenu(float from, float to, CanvasGroup PreviousMenu, CanvasGroup CurrentMenu)
    {
        PreviousMenu.blocksRaycasts = false;
         isFading = true;
        var curve = new AnimationCurve(new Keyframe[]
        {
            new Keyframe(1f, from),
            new Keyframe(0f, to)
        });

        float time = 1f;

        while (time > 0f)
        {
            PreviousMenu.alpha = curve.Evaluate(time);
            time -= 0.5f * Time.deltaTime;
            yield return null;
        }


        PreviousMenu.alpha = curve.Evaluate(0f);



        var curveSecond = new AnimationCurve(new Keyframe[]
       {
            new Keyframe(1f, from),
            new Keyframe(0f, to)
       });


        while (time < 1f)
        {
            CurrentMenu.alpha = curveSecond.Evaluate(time);
            time += 0.5f * Time.deltaTime;
            yield return null;
        }

        CurrentMenu.alpha = curveSecond.Evaluate(1f);

        isFading = false;

        CurrentMenu.blocksRaycasts = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeFromTo(0, 1f));
    }

    public void SettingsButton()
    {
        if(isFading == false)
        {
            settingsObj.SetActive(true);
            StartCoroutine(FadeFromToNewMenu(1f, 0, MainMenu, SettingsMenu));
        }
    }

    public void ExtrasButton()
    {
        if (isFading == false)
        {
            extrasObj.SetActive(true);
            StartCoroutine(FadeFromToNewMenu(1f, 0, MainMenu, ExtrasMenu));
        }
    }

    public void SettingsToMainMenu()
    {
        if(isFading == false)
        {
            StartCoroutine(FadeFromToNewMenu(1f, 0, SettingsMenu, MainMenu));
            
        }
    }

    public void ExtrasToMainMenu()
    {
        if (isFading == false)
        {
            StartCoroutine(FadeFromToNewMenu(1f, 0, ExtrasMenu, MainMenu));
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
