using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElements : MonoBehaviour
{

    public GameObject LevelCompletePanel;
    public static GameObject LevelCompletePanel_Reference;
    // Start is called before the first frame update
    void Start()
    {
        LevelCompletePanel_Reference = LevelCompletePanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
