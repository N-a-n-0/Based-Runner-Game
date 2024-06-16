using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Were in!!!!");
        // Check if the collider that entered the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            Debug.Log("Scene should change...");
            SceneManager.LoadScene(6);
        }
    }
}
