using System.Collections;
using System.Collections.Generic;
 

//using System.Diagnostics;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public int speedBoostDuration = 5; // Duration of the speed boost in seconds
    public float speedBoost = 20f; // Factor by which to multiply the player's speed

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply speed boost using the GameManager
            
            SpeedBoostManager.instance.ApplySpeedBoost(speedBoost, speedBoostDuration);

            // Optionally, destroy the speed panel after use
          //  Destroy(gameObject);
        }
    }
}
