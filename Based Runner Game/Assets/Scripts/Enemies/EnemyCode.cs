using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyCode : MonoBehaviour
{

    public GameObject enemyObj;
    

    public float enemyDistance_ZAxis;
    public float playerDistance_ZAxis;
    public float distanceRequired;
    public  Animator animator;

    public BoxCollider boxCollider;

    public float baseAnimationSpeed = 1f;  // Base speed of the attack animation
    public float animationSpeedFactor = 0.01f;  // Factor to adjust animation speed based on player's speed
    public float preemptiveDistanceFactor = 0.5f;  // Multiplier to adjust the trigger distance


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //print("Player Objects Local Position" + PlayerController.child_Obj_Reference.transform.localPosition.z);
        //print("Player Objects world Position" + PlayerController.child_Obj_Reference.transform.position.z);
        enemyDistance_ZAxis = enemyObj.transform.position.z;
        playerDistance_ZAxis = PlayerController.child_Obj_Reference.transform.position.z;// Vector3.Distance(enemyObj.transform.position, PlayerController.child_Obj_Reference.transform.position);

        // Calculate the adjusted trigger distance and animation speed
        float playerSpeed = PlayerController.forwardSpeed;
        float adjustedTriggerDistance = distanceRequired + (preemptiveDistanceFactor * playerSpeed);
        float adjustedAnimationSpeed = Mathf.Clamp(baseAnimationSpeed + (animationSpeedFactor * playerSpeed), baseAnimationSpeed, 2f * baseAnimationSpeed);


        if (enemyDistance_ZAxis < playerDistance_ZAxis + adjustedTriggerDistance) 
        {
            animator.speed = adjustedAnimationSpeed;

            switch (PlayerController.desiredLane)
            {
                case 0:
                    animator.SetBool("Lane0", true);
                    break;
                case 1:
                    animator.SetBool("Lane1", true);
                    break;

                case 2:
                    animator.SetBool("Lane2", true);
                    break;

            }
            animator.SetBool("playerDistance", true);
       //     print("Player is within enemies range!!!");

        }
    }


    public void activateHitBox()
    {
        boxCollider.enabled = true;
    }

    public void deactivateHitBox()
    {
        boxCollider.enabled = false;
    }

}
