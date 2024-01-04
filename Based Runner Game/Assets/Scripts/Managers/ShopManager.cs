using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour, IDataPersistence
{
    public int currentCharacterIndex = 0;
    public GameObject[] characterModels;

    public CharacterBlueprint[] characters;

    public Button buyButton;
    public TMP_Text coinText;

    

   




    public void LoadData(GameData data)
    {
        CoinManager.coinsCollected = data.coins;

        for (int i= 0; i< characters.Length; i++)
        {
           data.charactersUnlocked.TryGetValue(i, out characters[i].isUnlocked);
           
            if(characters[i].isUnlocked)
            {
                print("This character Index is unlocked  INDEX: " + i);
            }

          
        }
    }
    public void SaveData(GameData data)
    {
        data.coins = CoinManager.coinsCollected;


        for (int i = 0; i < characters.Length; i++)
        {
           
            if(data.charactersUnlocked.ContainsKey(i))
            {
                data.charactersUnlocked.Remove(i);
            }
            data.charactersUnlocked.Add(i, characters[i].isUnlocked);
        }
    }

 

    [ContextMenu("Generate guid for id")]

    public void GenerateGuid(CharacterBlueprint character)
    {
        character.id = System.Guid.NewGuid().ToString();
    }


  
    void Start()
    {
        
      
      
        int currentPos = 0;

        foreach(CharacterBlueprint character in characters)
         {
           
            if (character.price == 0)
            {
                character.isUnlocked = true;
            }
            else
            {
               
            
            }

            currentPos++;

        }
       


     
        foreach (GameObject character in characterModels)
        {
            character.SetActive(false);
        }

        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        characterModels[currentCharacterIndex].SetActive(true);
       
    }

    void Update()
    {
       
       
        if(coinText != null)
        {
            coinText.GetComponentInChildren<TextMeshProUGUI>().text = "Coins: " + CoinManager.coinsCollected;
            UpdateUI();
        }

    }
   
   
    public void UnlockCharacter()
    {
        CharacterBlueprint characterPlayerModel = characters[currentCharacterIndex];



      


        
        characterPlayerModel.isUnlocked = true;
        CoinManager.coinsCollected -= characterPlayerModel.price;

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

        CharacterBlueprint characterPlayerModel = characters[currentCharacterIndex];
        if(!characterPlayerModel.isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }

    public void ShowCurrentCharacter()
    {
        characterModels[currentCharacterIndex].SetActive(false);
        int CurrentSelectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        characterModels[CurrentSelectedCharacter].SetActive(true);
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

        CharacterBlueprint characterPlayerModel = characters[currentCharacterIndex];
        if (!characterPlayerModel.isUnlocked)
        {
            return;
        }
        
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }


    private void UpdateUI()
    {
        CharacterBlueprint c = characters[currentCharacterIndex];
        if(c.isUnlocked)
        {
          
            buyButton.gameObject.SetActive(false);
        }
        else
        {

            buyButton.gameObject.SetActive(true);
          
                buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy - " + c.price;
           
            if (c.price < CoinManager.coinsCollected)
                {
                buyButton.interactable = true;
                }
                else
                {
                buyButton.interactable = false;
                }

        }

    }



   
}
