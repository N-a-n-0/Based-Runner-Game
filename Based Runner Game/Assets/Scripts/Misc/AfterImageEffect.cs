using System.Collections;
using System.Collections.Generic;
 

//using System.Diagnostics;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    public bool applyRainbowEffect;
    //public bool disableMainRender;

    public GameObject transformTarget; // Object to set the duplicated object's transform to
    public GameObject parentObject; // Object to set as the parent of the duplicated objects
    public GameObject objectToDuplicate;

    //public GameObject mainRenderObj;

    public Color afterImageColor = new Color(1f, 1f, 1f, 0.5f); // Adjust alpha as needed
    public float spawnDistance = 1.0f; // Distance between each afterimage
    public float lifespan = 2.0f; // Time before the afterimage object is destroyed
    public Shader afterImageShader; // Shader to use for the afterimage effect

    [Range(0, 1)]
    public float initialAlpha = 0.5f; // Control the starting alpha in the Inspector

    [Range(0, 10)]
    public float rainbowColorChangeSpeed = 1.0f; // Control how fast the rainbow colors change

    public int maxAfterImages = 10; // Maximum number of afterimages allowed

    [Range(0.1f, 10.0f)]
    public float fadeSpeed = 1.0f; // New variable to control the fade speed

    private Vector3 lastPosition; // The last recorded position of the player
    private float hueShift = 0.0f; // Hue shift for the rainbow effect
    private List<GameObject> afterImages = new List<GameObject>(); // List to track all spawned afterimages

    void Start()
    {
        lastPosition = transformTarget.transform.position; // Initialize last position
    }

    void FixedUpdate()
    {
        
        if (SpeedBoostManager.speeedApplied)
        {
            // Calculate the distance moved since the last afterimage was spawned
            float distanceMoved = Vector3.Distance(transformTarget.transform.position, lastPosition);

            // Spawn afterimages as long as the distance moved exceeds spawnDistance
            while (distanceMoved >= spawnDistance)
            {
            //    if (disableMainRender)
             //   {
                 //   mainRenderObj.SetActive(false);
            // //   }

                lastPosition += (transformTarget.transform.position - lastPosition).normalized * spawnDistance;

                // Ensure the maximum number of afterimages is not exceeded
                if (afterImages.Count >= maxAfterImages)
                {
                    // Remove the oldest afterimage
                    GameObject oldestAfterImage = afterImages[0];
                    afterImages.RemoveAt(0);
                    Destroy(oldestAfterImage);
                }

                GameObject duplicatedObject = Instantiate(objectToDuplicate);
                duplicatedObject.transform.SetParent(parentObject.transform, false); // Ensure consistent scale
                Transform cube = duplicatedObject.transform.GetChild(0); // 0 is the index of the first child

                cube.gameObject.SetActive(true);

                if (transformTarget != null)
                {
                    duplicatedObject.transform.position = lastPosition;
                    duplicatedObject.transform.rotation = transformTarget.transform.rotation;
                    duplicatedObject.transform.localScale = objectToDuplicate.transform.localScale;
                }

                Animator animator = duplicatedObject.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.enabled = false;
                }

                Destroy(duplicatedObject.GetComponent<PlayerFunctions>());
                Destroy(duplicatedObject.GetComponent<GameOverAnimation>());

                AdjustMaterial(duplicatedObject);

                afterImages.Add(duplicatedObject);

                StartCoroutine(FadeAndDestroy(duplicatedObject, lifespan, fadeSpeed));

                // Recalculate the distance moved since last afterimage was spawned
                distanceMoved = Vector3.Distance(transformTarget.transform.position, lastPosition);
            }
        }
        else
        {
            // Reactivate the main render object if the condition is not met
            if (!PlayerManager.gameOver)
            {
             //   mainRenderObj.SetActive(true);
            }
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

                // Use the static forwardSpeed variable directly
                float speedFactor = PlayerController.forwardSpeed / 100f; // Adjust 100f as needed
                Color color = applyRainbowEffect ? GetRainbowColor(hueShift) : afterImageColor;
                color.a = initialAlpha; // Set the initial alpha value
                material.SetColor("_TintColor", color);

                if (applyRainbowEffect)
                {
                    hueShift += rainbowColorChangeSpeed * Time.deltaTime; // Control the speed of color change
                    if (hueShift > 1.0f) hueShift -= 1.0f;
                }
            }
        }
    }

    IEnumerator FadeAndDestroy(GameObject afterImage, float duration, float fadeSpeed)
    {
        SkinnedMeshRenderer skinnedMeshRenderer = afterImage.transform.Find("Cube").GetComponent<SkinnedMeshRenderer>();
        Material material = skinnedMeshRenderer.material;

        Color initialColor = material.GetColor("_TintColor");
        float elapsedTime = 0.0f;
        float fadeRate = duration / fadeSpeed; // Adjust the fade rate using fadeSpeed

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / fadeRate);
            material.SetColor("_TintColor", new Color(initialColor.r, initialColor.g, initialColor.b, alpha));
            yield return null;
        }

        Destroy(afterImage);
    }
}
