using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public int currentCharacterIndex = 0;
    public GameObject[] characters;

    // Start is called before the first frame update
    void Start()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }


        characters[currentCharacterIndex].SetActive(true);

    }

}
