using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(80 * Time.deltaTime, 0, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("THIS RAN");
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            PlayerManager.numberOfCoins += 1;
            PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0) + 1);
            Destroy(gameObject);

            Debug.Log(PlayerManager.numberOfCoins);
        }
    }
}
