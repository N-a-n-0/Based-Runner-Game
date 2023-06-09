using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    private Vector3 offset;
    public int currentCharacterIndex = 0;

   // public Camera publicMainCameraVar;
   // public Camera publicCutSceneCameraVar;

    [SerializeField]
    public  Camera mainCamera;
    [SerializeField]
    public  Camera cutSceneCamera;

    public GameObject[] characters;

   // public GameObject[] characters =  CharacterSelector.characterTransforms;

    void Start()
    {
        target = characters[PlayerPrefs.GetInt("SelectedCharacter", 0)].transform;
        offset = transform.position - target.position;   
    }

    // Update is called once per frame
    void Update()
    {
      
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z - 1);
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);

    }
}
