using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("bruh");
        if(RenderSettings.skybox == SkyBoxChange.NightRef)
        {

            RenderSettings.skybox = SkyBoxChange.DayRef;
        }
        else
        {
            RenderSettings.skybox = SkyBoxChange.NightRef;
        }
        
    }
}
