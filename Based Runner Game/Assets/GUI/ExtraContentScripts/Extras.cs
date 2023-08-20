using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Extras : MonoBehaviour, IDataPersistence
{

   public  ExtrasBluePrint[] extras;
    public GameObject[] Art;
    public GameObject[] ArtLocation;

    public void LoadData(GameData data)
    {
       
          for (int i = 0; i < extras.Length; i++)
          {
              data.Extras_.TryGetValue(i, out extras[i].isUnlocked);
              
              print(data.Extras_.TryGetValue(i, out extras[i].isUnlocked) + "CURRENT: " + i);
              if (extras[i].isUnlocked)
              {
                print("THIS ARTWORK IS UNLCOKED");
              }

              print("RESULTS:" + extras[i].isUnlocked);
          }
    }
    public void SaveData(GameData data)
    {
        
        


        for (int i = 0; i < extras.Length; i++)
        {

            if (data.Extras_.ContainsKey(i))
            {
                data.Extras_.Remove(i);
            }
            data.Extras_.Add(i, extras[i].isUnlocked);
        }
    }

    public GameObject[] ArtList()
    {
        return Art;
    }

    public  Image CheckExtraOut(int Art_Number)
    {

    //  print( .sprite);
        print(extras[Art_Number].name);
        return Art[Art_Number].GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int currentIndex = 0;

        if(SceneManager.GetActiveScene().name == "Menu")
        {
            foreach (ExtrasBluePrint extra in extras)
            {
                if (extra.isUnlocked)
                {
                    ArtLocation[currentIndex].GetComponent<Image>().sprite = Art[currentIndex].GetComponent<Image>().sprite;
                }
                currentIndex++;
            }

        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

}
