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
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.isGameStarted == true && scoreIncreasing == true && PlayerManager.gameOver == false)
        {


            currentScore += pointsPerSecond * Time.deltaTime;
            scoreText.text = "Score: " + Mathf.Round(currentScore);
        }
      
    }
}
