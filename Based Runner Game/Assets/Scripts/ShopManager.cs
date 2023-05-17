using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int currentCharacterIndex = 0;
    public GameObject[] characterModels;

    // Start is called before the first frame update
    void Start()
    {
        print(PlayerPrefs.GetInt("SelectedCharacter", 0));
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach(GameObject character in characterModels)
        {
            character.SetActive(false);
        }


        characterModels[currentCharacterIndex].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNext()
    {
        characterModels[currentCharacterIndex].SetActive(false);

        currentCharacterIndex++;

        if(currentCharacterIndex == characterModels.Length)
        {
            currentCharacterIndex = 0;
        }
        characterModels[currentCharacterIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }

    public void ChangePrevious()
    {
        characterModels[currentCharacterIndex].SetActive(false);

        currentCharacterIndex--;

        if (currentCharacterIndex ==  - 1)
        {
            currentCharacterIndex = characterModels.Length - 1;
        }
        characterModels[currentCharacterIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }

}
