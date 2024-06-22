using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;


public class SettingsMenuManager : MonoBehaviour
{
    public static bool isVibrate;

    public TMP_Dropdown graphicsDropdown;
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;
    public Toggle vibrateToggle;


    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSfxVolume()
    {
        mainAudioMixer.SetFloat("SfxVol", sfxVol.value);
    }

    public void ChangeVibrate()
    {
        isVibrate = vibrateToggle;
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    // Start is called before the first frame update
    void Start()
    {
        isVibrate = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
