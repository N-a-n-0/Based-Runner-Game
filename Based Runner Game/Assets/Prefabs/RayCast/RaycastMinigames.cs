using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMinigames : MonoBehaviour
{
    public Camera myCamera;


    //public Renderer _renderer;

    public Ray ray;
    public RaycastHit hit;

    static int buttonselected;

    public static bool start = false;

    public enum ManageClick
    {
        NONE,
        DOWN,
        HELD,
        UP,
    }

    [SerializeField]
    public ManageClick myManageClick;
    // Start is called before the first frame update
    void Start()
    {
        MicroGameManager.MiniG_2 = this;
        //  myCamera = Camera.main;
        //  _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MicroGameManager.ActiveCoroutine == null && MicroGameManager.selectedMicroGame == 1 && CameraFunctions.MicroGameStartReached == true && start == false)
        {
            start = true;
            StartMiniGame();
        }

        if (Input.GetMouseButtonDown(0) && MicroGameManager.alreadyEntered == true && MicroGameManager.AnswerPicked == false)
        {
            HandleClickMouse();
        }
    }

    public void Success()
    {
        MicroGameManager.ObjectiveText = "Success!!!";
        MicroGameManager.UniversalObjectiveTextAnimReference.SetBool("Success", true);
     //   MicroGameManager.AnswerPicked = true;
        print("cube was clicked");
        start = false;
    }

    public void Failure()
    {
        MicroGameManager.ObjectiveText = "Failure!!!";
        MicroGameManager.UniversalObjectiveTextAnimReference.SetBool("Failure", true);
      //  MicroGameManager.AnswerPicked = true;
        print("cube was clicked");
        start = false;
    }

    void HandleRayCast(Vector3 myInput)
    {
        ray = myCamera.ScreenPointToRay(myInput);
        if (Physics.Raycast(ray, out hit))
        {
            // print("We touching a object and the name of the object is :" + hit.transform.gameObject.name);
            switch (hit.transform.gameObject.tag)
            {
                case "RedCube":

                    //we need to make sure the animator overrides with an animatoroverrides

                    //animtion activiation right here determine win or loss (Make game logic) 
                    
                    if(buttonselected == 0 )
                    {

                        Success();
                        MicroGameManager.AnswerPicked = true;
                    }
                    else
                    {
                        Failure();
                        MicroGameManager.AnswerPicked = true;
                    }
                     
                    //  hit.transform.position += Vector3.right;
                    break;
                case "BlueCube":
                    if (buttonselected == 1)
                    {
                        Success();
                        MicroGameManager.AnswerPicked = true;
                    }
                    else
                    {
                        Failure();
                        MicroGameManager.AnswerPicked = true;
                    }
                    //  hit.transform.position += Vector3.down; 
                    break;
            }

        }
    }



    void HandleClickMouse()
    {
        switch (myManageClick)
        {
            case ManageClick.NONE:
                break;
            case ManageClick.DOWN:
                if (Input.GetMouseButtonDown(0))
                {
                    HandleRayCast(Input.mousePosition);
                }
                break;
            /*
        case ManageClick.HELD:
            if(Input.GetMouseButton(0))
            {
                HandleRayCast(Input.mousePosition);
            }
            break;
        case ManageClick.UP:
            if(Input.GetMouseButtonUp(0))
            {
                HandleRayCast(Input.mousePosition);
            }
            break;
            */
            default:
                break;
        }
    }

    public void StartMiniGame()
    {
         buttonselected = UnityEngine.Random.Range(0, 2);


        switch(buttonselected)
        {
            case 0:
                MicroGameManager.ObjectiveText = "Pick red";
                break;
            case 1:
                MicroGameManager.ObjectiveText = "Pick blue";
                break;
            default:
                print("This really shouldn't have happened");
                break;
        }

        
        print(buttonselected);
    }
}
