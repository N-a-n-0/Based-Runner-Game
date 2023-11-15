using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public Transform target;
    private Vector3 offset;
    public int currentCharacterIndex = 0;


    [SerializeField]
    public  Camera mainCamera;
    [SerializeField]
    public  Camera cutSceneCamera;

    public GameObject[] characters;

        


   

    public void CullingMaskGameOverSettings()
    {
        mainCamera.cullingMask = LayerMask.GetMask("Ground", "Player");

    }

}
