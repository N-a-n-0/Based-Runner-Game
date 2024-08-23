using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
      static float touchStartTime = 0f;
    private float maxTapDistance = 50f;

    public TMP_Text InputCheckText;

    private void Update()
    {
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;
        #region Standalone Inputs
        
        if (Input.GetMouseButtonDown(0))
        {
         //   touchStartTime = 0;
            touchStartTime = Time.unscaledTime;
          //  print("Screen has been just touched");
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // print("Screen touch was released");

            //CHECKING TAP HERE

            touchStartTime = Time.unscaledTime - touchStartTime;
            print(touchStartTime);
           
         //   print("swipeDelta.magnitude" + swipeDelta.magnitude);
            
            swipeDelta = (Vector2)Input.mousePosition - startTouch;


            if ((swipeDelta.magnitude < maxTapDistance) && touchStartTime < 0.15f && MicroGameManager.alreadyEntered == false) ////(swipeLeft == false || swipeRight == false || swipeDown == false || swipeUp == false) 
            {
                PlayerController.forwardSpeed += .2f;
                print("this is a tap ");
                InputCheckText.text = "TAPPED, Total Speed is: " + PlayerController.forwardSpeed;
            }
            else
            {
                print("this is a swipe");
                InputCheckText.text = "SWIPED, Total Speed is: " + PlayerController.forwardSpeed;
            }
            //********************************************************



            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {

         Touch touch = Input.touches[0];


            if (touch.phase == TouchPhase.Began)
            {
                touchStartTime = Time.unscaledTime;
                tap = true;
                isDraging = true;
                startTouch = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {

                //For some reason this code for detecting swipes does not work for the mobile side of things although I think Mouse inputs translate to mobile touch screen inputs (BUILD THE PROJEECT AND TEST IF THIS THEORY IS TRUE)
                /*
                float touchDuration = Time.time - touchStartTime; // Calculate the duration
                swipeDelta = touch.position - startTouch; // Calculate the distance

                if ((swipeDelta.magnitude < maxTapDistance) && touchStartTime < 0.15f) ////(swipeLeft == false || swipeRight == false || swipeDown == false || swipeUp == false) 
                {
                    print("this is a tap ");
                }
                else
                {
                    print("this is a swipe");
                }
               */

                isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate the distance
        CalculateDistance();

        //Did we cross the distance?

        if (swipeDelta.magnitude > 40)
        {
            print("swipeDelta.magnitude" + swipeDelta.magnitude);
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
          
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    
    public float CalculateDistance()
    {
        swipeDelta = Vector2.zero;
        if (isDraging)
        {

            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }
        
        return swipeDelta.magnitude;
    }

}