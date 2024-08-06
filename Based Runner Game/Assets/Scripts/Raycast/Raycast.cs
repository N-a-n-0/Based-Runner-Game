using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;

public class Raycast : MonoBehaviour
{

    public Camera myCamera;

    //public Renderer _renderer;

    public Ray ray;
    public RaycastHit hit;

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
      //  myCamera = Camera.main;
      //  _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MicroGameManager.alreadyEntered == true) 
        {
            HandleClickMouse();
         }
        }


    void HandleRayCast(Vector3 myInput)
    {
        ray  = myCamera.ScreenPointToRay(myInput);
        if(Physics.Raycast(ray, out hit))
        {
           // print("We touching a object and the name of the object is :" + hit.transform.gameObject.name);
            switch (hit.transform.gameObject.tag)
            {
                case "RedCube":
                    print("redcube was clicked");
                  //  hit.transform.position += Vector3.right;
                    break;
                case "BlueCube":
                    print("bluecube was clicked");
                    //  hit.transform.position += Vector3.down; 
                    break;
            }

        }
    }

    void HandleClickMouse()
    {
        switch(myManageClick)
        {
            case ManageClick.NONE:
                break;
            case ManageClick.DOWN:
                if(Input.GetMouseButtonDown(0))
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

    /*
    void IsClickedObject()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleClickMouse();
#elif UNITY_ANDROID || UNITY_IOS
        HandleTouchMobile();
#endif
    }
    */
    /*
    void HandleTouchMobile()
    {
        switch (myManageClick)
        {
            case ManageClick.NONE:
                break;
            case ManageClick.DOWN:
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    HandleRayCast(Input.toches[0].position);
                }
                break;
            case ManageClick.HELD:
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
                {
                    HandleRayCast(Input.toches[0].position);
                }
                break;
            case ManageClick.UP:
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
                {
                    HandleRayCast(Input.toches[0].position);
                }
                break;
            default:
                break;
        }
    }
    */



}

