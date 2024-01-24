using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxChange : MonoBehaviour
{
   
    public Material Day;
    public Material Night;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeToDay()
    {
        RenderSettings.skybox = Day;
    }
    public void ChangeToNight()
    {
        RenderSettings.skybox = Night;
    }

}
