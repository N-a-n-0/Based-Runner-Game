using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour, IDataPersistence
{

    public int currentCharacterIndex = 0;
    public  GameObject[] characters;
    public GameObject camera;

    public static GameObject CurrentSelectedCharacter;

  
    public void LoadData(GameData data)
    {

    }

    public void SaveData(GameData data)
    {

    }


    void Start()
    {
       
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }


        characters[currentCharacterIndex].SetActive(true);
        CurrentSelectedCharacter = characters[currentCharacterIndex];
        camera.transform.parent = characters[currentCharacterIndex].transform;
    }

    

}
