using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IDataPersistence
{
    public static int coinsCollected;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(80 * Time.deltaTime, 0, 0);
    }
    
    public void LoadData(GameData data)
    {
        coinsCollected = data.coins;
        
    }
    public void SaveData(GameData data)
    {
        data.coins = coinsCollected;

    }

    public int getCoins()
        { return coinsCollected; }

    private void OnTriggerEnter(Collider other)
    {
       
        print("NUTS" + coinsCollected);
        if(other.tag == "Player")
        {
            print("THIS RAN");
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            coinsCollected++;
           
            PlayerManager.numberOfCoins += 1;
            //PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0) + 1);
            Destroy(gameObject);

            Debug.Log(coinsCollected);
        }
    }
}
