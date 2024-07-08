using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixer mainAudioMixerInGame;

    public AudioMixerGroup audioMixerGroupMusic;
    public AudioMixerGroup audioMixerGroupSFX;

    void Start()
    {
      //  AudioMixerGroup[] audioMixerGroups = mainAudioMixerInGame.FindMatchingGroups("MainAudioMixer");


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            if(s.Music == true)
            {
                print("This is a song");
                s.source.outputAudioMixerGroup = audioMixerGroupMusic;
            }
            else if(s.SFX == true) 
            {
                print("This is a Sound Effect");
                s.source.outputAudioMixerGroup = audioMixerGroupSFX;
            }
            else
            {
                print("THIS SOUND OR SONG DOES NOT HAVE AN AUDIO GROUP MIXER ASSIGNED!!! SETTINGS SET BY PLAYER MAY BE IGNORED !!!");
            }

          //  s.source.outputAudioMixerGroup = "MasterVol";

        }
        PlaySound("MainTheme");

        mainAudioMixerInGame.SetFloat("MasterVol", PlayerPrefs.GetFloat("master"));
        mainAudioMixerInGame.SetFloat("MusicVol", PlayerPrefs.GetFloat("music"));
        mainAudioMixerInGame.SetFloat("SfxVol", PlayerPrefs.GetFloat("sfx"));

    }

   public void PlaySound(string name)
    {

        foreach (Sound s in sounds)
        {
            if(s ==null)
            {
         //       Debug.LogWarning("Sound: " + name + " not found!");
            }

            if (s.name == name)
            {
         //       print(s.volume);
               
                s.source.Play();
            }
               
                
               
        }
    }

    public void StopSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
         //       print(s.volume);
               
                s.source.Stop();
            }

            

        }

      
    }
}
