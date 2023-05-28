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

    public int coin;

    

    

    public void LoadData(GameData data)
    {
        coin = data.coins;
    }
    public void SaveData(GameData data)
    {
        data.coins = coin;

    }



    // Start is called before the first frame update
    void Start()
    {

        foreach(CharacterBlueprint character in characters)
         {
           
            if (character.price == 0)
            {
                character.isUnlocked = true;
            }
            else
            {
               
              character.isUnlocked = PlayerPrefs.GetInt(character.name, 0)==0 ? false: true;
            }
        }

        //print(PlayerPrefs.GetInt("SelectedCharacter", 0));
       // currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
      // currentCharacterIndex = 
        foreach(GameObject character in characterModels)
        {
            character.SetActive(false);
        }


        characterModels[currentCharacterIndex].SetActive(true);

    }

    void Update()
    {
       // coin++;
       coinText.GetComponentInChildren<TextMeshProUGUI>().text = "Coins: " + coin;
        UpdateUI();
    }
    // Update is called once per frame
   
    public void UnlockCharacter()
    {
        CharacterBlueprint characterPlayerModel = characters[currentCharacterIndex];

        //   PlayerPrefs.SetInt(characterPlayerModel.name, 1);
        //   PlayerPrefs.SetInt("SelectedCar",currentCharacterIndex);
        //  characterPlayerModel.isUnlocked = true;
        // PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0 ) - characterPlayerModel.price);



        characterPlayerModel.isUnlocked = true;
        coin -= characterPlayerModel.price;

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
           
            if (c.price < Coin.coinsCollected)
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
