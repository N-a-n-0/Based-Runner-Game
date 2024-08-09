using System.Collections;
using System.Collections.Generic;
 

//using System.Diagnostics;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    public bool applyRainbowEffect;
    public bool disableMainRender;
   // public bool SingleObject;

    public GameObject transformTarget; // Object to set the duplicated object's transform to
    public GameObject parentObject; // Object to set as the parent of the duplicated objects
    public GameObject objectToDuplicate;

  //  public GameObject SingleObj;

    public GameObject mainRenderObj;

    public Color afterImageColor = new Color(1f, 1f, 1f, 0.5f); // Adjust alpha as needed
    public float spawnInterval = 0.5f; // Time between spawns
    public float lifespan = 2.0f; // Time before the afterimage object is destroyed
    public Shader afterImageShader; // Shader to use for the afterimage effect

    private float timer = 0.0f; // Timer to track time between spawns
    private float hueShift = 0.0f; // Hue shift for the rainbow effect

    void Update()
    {
        // Update the timer
        // if(TempSpeedBoost)
        if (SpeedBoostManager.speeedApplied == true)
        {
            timer += Time.deltaTime;
            print("Mutiplate Duplication Method");
            // Check if it's time to spawn a new afterimage
            if (timer >= spawnInterval)
            {
                if (disableMainRender == true)
                {
                    mainRenderObj.SetActive(false);

                }


                // Reset the timer
                timer = 0.0f;

                // Duplicate the object including its children
                GameObject duplicatedObject = Instantiate(objectToDuplicate);

                // Set the duplicated object as a child of the parentObject
                duplicatedObject.transform.SetParent(parentObject.transform);
                Transform cube = duplicatedObject.transform.GetChild(0); // 0 is the index of the first child

                // Set the child active or inactive
                cube.gameObject.SetActive(true); // Or false to deactivate


                // Match the transform of the duplicated object to the transformTarget
                if (transformTarget != null)
                {
                    duplicatedObject.transform.position = transformTarget.transform.position;
                    duplicatedObject.transform.rotation = transformTarget.transform.rotation;
                    duplicatedObject.transform.localScale = transformTarget.transform.localScale;
                }

                // Disable the Animator component on the duplicated object
                Animator animator = duplicatedObject.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.enabled = false;
                }

                // Remove the main parent script from the duplicated object
                Destroy(duplicatedObject.GetComponent<PlayerFunctions>()); // Replace PlayerFunctions with the script to remove
                Destroy(duplicatedObject.GetComponent<GameOverAnimation>());

                // Adjust the material and shader of the duplicated object
                AdjustMaterial(duplicatedObject);

                // Destroy the duplicated object after the specified lifespan
                Destroy(duplicatedObject, lifespan);
            }


        }
      
        else
        {
           
            if (PlayerManager.gameOver == false)
            {
                mainRenderObj.SetActive(true);
            }


        }


        Color GetRainbowColor(float hue)
        {
            return Color.HSVToRGB(hue, 1.0f, 1.0f); // Saturation and Value are set to 1 for full brightness
        }

        void AdjustMaterial(GameObject root)
        {
            // Find the Cube child object
            Transform cubeTransform = root.transform.Find("Cube");
            if (cubeTransform != null)
            {
                SkinnedMeshRenderer skinnedMeshRenderer = cubeTransform.GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer != null)
                {
                    // Change the material shader and color to create the afterimage effect
                    Material material = skinnedMeshRenderer.material;
                    material.shader = afterImageShader;


                    if (applyRainbowEffect == true)
                    {
                        Color rainbowColor = GetRainbowColor(hueShift);
                        material.SetColor("_TintColor", rainbowColor);
                        hueShift += 0.05f; // Adjust the increment value to control the speed of color cycling
                        if (hueShift > 1.0f) hueShift -= 1.0f;
                    }
                    else
                    {
                        Color color = afterImageColor;
                        material.SetColor("_TintColor", color);
                    }

                    // Set the color and alpha using the _TintColor property

                }
            }
        }
    }
}
