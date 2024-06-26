using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
//using static System.Net.Mime.MediaTypeNames;



public class MicroMath : MonoBehaviour
{

    public GameObject ParentObj;

    public TMP_Text ProblemText;

    public TMP_Text Objective;



    public Button[] Answers;
 
    private TMP_Text buttonText_1;
    private TMP_Text buttonText_2;
    private TMP_Text buttonText_3;

    void Start()
    {
        print("START FUNCTION RAN NICE");
        Objective.text = "SOLVE IT";
        buttonText_1 = Answers[0].GetComponentInChildren<TMP_Text>();
        buttonText_2 = Answers[1].GetComponentInChildren<TMP_Text>();
        buttonText_3 = Answers[2].GetComponentInChildren<TMP_Text>();
        GenerateAnswer();



    }


    public void GenerateAnswer()
    {
       

      //  MicroGame.timeNeeded = 5f;
        int x = Random.Range(1, 10);
        int y = Random.Range(1, 10);

        int answer = x + y;

        ProblemText.text = x + " + " + y;

        print(x + " + " + y
             + "Answer is: " + answer);
        int correctPick = UnityEngine.Random.Range(0, 2) + 1;
        print("Correct pick button number is:" + correctPick);

       

     
        //Continue this test micro minigame 

        //Figure out how to make the answer buttons randoms and not static everytime 
        if (correctPick == 0) 
        {
            Answers[correctPick].onClick.AddListener(CorrectPick); //Left button is the answer
            buttonText_1.text = answer.ToString();

             Answers[1].onClick.AddListener(IncorrectPick);
            buttonText_2.text = (answer + UnityEngine.Random.Range(1, 3)).ToString();

            Answers[2].onClick.AddListener(IncorrectPick);
            buttonText_3.text = (answer + UnityEngine.Random.Range(4, 5)).ToString();

        }
        else if(correctPick == 1)
        {
            Answers[correctPick].onClick.AddListener(CorrectPick); //Middle button is the answer
            buttonText_2.text = answer.ToString();

            Answers[0].onClick.AddListener(IncorrectPick);
            buttonText_1.text = (answer + UnityEngine.Random.Range(1, 3)).ToString();

            Answers[2].onClick.AddListener(IncorrectPick);
            buttonText_3.text = (answer + UnityEngine.Random.Range(4, 5)).ToString();
        }
        else
        {
            Answers[correctPick].onClick.AddListener(CorrectPick); // Right button is the answer
            buttonText_3.text = answer.ToString();

            Answers[1].onClick.AddListener(IncorrectPick);
            buttonText_2.text = (answer + UnityEngine.Random.Range(1, 3)).ToString();

            Answers[0].onClick.AddListener(IncorrectPick);
            buttonText_1.text = (answer + UnityEngine.Random.Range(4, 5)).ToString();
        }

    }

   
        

    public void CorrectPick()
    {

        print("This is the answer button");



        for(int i = 0; i < Answers.Length; i++)
        {
            Answers[i].onClick.RemoveListener(CorrectPick);
        }
        for (int i = 0; i < Answers.Length; i++)
        {
            Answers[i].onClick.RemoveListener(IncorrectPick);
        }
    }

    public void IncorrectPick()
    {
        print("This is the incorrect answer button");
        for (int i = 0; i < Answers.Length; i++)
        {
            Answers[i].onClick.RemoveListener(CorrectPick);
        }

        for (int i = 0; i < Answers.Length; i++)
        {
            Answers[i].onClick.RemoveListener(IncorrectPick);
        }
    }

    // Start is called before the first frame update
   


    // Update is called once per frame
    void Update()
    {
        
    }
}
