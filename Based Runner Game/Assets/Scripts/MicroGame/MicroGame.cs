using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGame : MonoBehaviour
{
    float curSpeed = 25;
    float savedSpeed = 25;
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
            Debug.Log("Collision: CURSPEED " + curSpeed);
        }
        PlayerController.forwardSpeed = 0;

        StartCoroutine(returnSpeedPlz());
    }

    private IEnumerator returnSpeedPlz()
    {
        yield return new WaitForSeconds(.5f);
        PlayerController.forwardSpeed = savedSpeed;
    }
}
