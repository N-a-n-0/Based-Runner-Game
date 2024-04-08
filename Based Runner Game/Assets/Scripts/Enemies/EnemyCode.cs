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
    // Start is called before the first frame update
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

        if (enemyDistance_ZAxis < playerDistance_ZAxis + distanceRequired) 
        {
            animator.SetBool("playerDistance", true);
            print("Player is within enemies range!!!");

        }
    }
}
