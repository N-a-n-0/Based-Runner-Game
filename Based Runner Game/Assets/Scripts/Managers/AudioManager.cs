using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
   
    void Start()
    {
     
        
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        PlaySound("MainTheme");

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
