using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class vibrateDevice : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickVibrate()
    {
        if (SettingsMenuManager.isVibrate)
        {


            Handheld.Vibrate();
            Debug.Log("Vibrate");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
