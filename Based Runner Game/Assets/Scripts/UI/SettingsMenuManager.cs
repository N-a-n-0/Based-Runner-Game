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
        PlayerPrefs.SetFloat("master", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
        PlayerPrefs.SetFloat("music", musicVol.value);
    }

    public void ChangeSfxVolume()
    {
        mainAudioMixer.SetFloat("SfxVol", sfxVol.value);
        PlayerPrefs.SetFloat("sfx", sfxVol.value);
    }

    public void ChangeVibrate()
    {
        isVibrate = vibrateToggle;
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }


    public void LoadMasterAudio()
    {
        masterVol.value = PlayerPrefs.GetFloat("master");
        
    }

    public void LoadMusicAudio()
    {
        musicVol.value = PlayerPrefs.GetFloat("music");
    }

    public void LoadSfxAudio()
    {
        sfxVol.value = PlayerPrefs.GetFloat("sfx");
    }


    // Start is called before the first frame update
    void Start()
    {

    isVibrate = true;
    mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    mainAudioMixer.SetFloat("SfxVol", sfxVol.value);

        if (PlayerPrefs.HasKey("master"))
        {
            LoadMasterAudio();
        }
       
        if(PlayerPrefs.HasKey("music"))
        {
        LoadMusicAudio();
        }

        if(PlayerPrefs.HasKey("sfx"))
        {
        LoadSfxAudio();
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
