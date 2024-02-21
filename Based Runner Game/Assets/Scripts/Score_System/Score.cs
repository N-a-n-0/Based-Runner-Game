using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
public class Score : MonoBehaviour
{

    public TMP_Text scoreText;

    public static float currentScore;

    public float pointsPerSecond;

    public static bool scoreIncreasing;

    // Start is called before the first frame update
    void Start()
    {
        scoreIncreasing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.isGameStarted == true && scoreIncreasing == true)
        {


            currentScore += pointsPerSecond * Time.deltaTime;
           
        }
        scoreText.text = "Score: " + Mathf.Round(currentScore);
    }
}
