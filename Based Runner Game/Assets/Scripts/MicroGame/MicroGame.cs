using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGame : MonoBehaviour
{
    float curSpeed = 25;
    float savedSpeed = 25;

    public Animator animator;


    public Camera MainCamera;

    public Camera MicroGameCamera;
    void Update()
    {
        curSpeed = PlayerController.forwardSpeed;
        Debug.Log("CURSPEED " + curSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        savedSpeed = curSpeed;
        if (other.CompareTag("Player"))
        {
            MainCamera.enabled = false;
            MicroGameCamera.enabled = true;
            Debug.Log("Collision: CURSPEED " + curSpeed);
            animator.SetBool("Touched", true);
        }
        PlayerController.forwardSpeed = 0;

        StartCoroutine(returnSpeedPlz());
    }

    private IEnumerator returnSpeedPlz()
    {
        yield return new WaitForSeconds(3f);
        MainCamera.enabled = true;
        MicroGameCamera.enabled = false;
        PlayerController.forwardSpeed = savedSpeed;
    }
}
