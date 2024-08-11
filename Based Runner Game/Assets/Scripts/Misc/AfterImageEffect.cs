using System.Collections;
using System.Collections.Generic;
 

//using System.Diagnostics;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    public bool applyRainbowEffect;
    public bool disableMainRender;

    public GameObject transformTarget; // Object to set the duplicated object's transform to
    public GameObject parentObject; // Object to set as the parent of the duplicated objects
    public GameObject objectToDuplicate;

    public GameObject mainRenderObj;

    public Color afterImageColor = new Color(1f, 1f, 1f, 0.5f); // Adjust alpha as needed
    public float spawnDistance = 1.0f; // Distance between each afterimage
    public float lifespan = 2.0f; // Time before the afterimage object is destroyed
    public Shader afterImageShader; // Shader to use for the afterimage effect

    [Range(0, 1)]
    public float initialAlpha = 0.5f; // Control the starting alpha in the Inspector

    [Range(0, 10)]
    public float rainbowColorChangeSpeed = 1.0f; // Control how fast the rainbow colors change

    private Vector3 lastPosition; // The last recorded position of the player
    private float hueShift = 0.0f; // Hue shift for the rainbow effect
    private List<GameObject> afterImages = new List<GameObject>(); // List to track all spawned afterimages

    void Start()
    {
        lastPosition = transformTarget.transform.position; // Initialize last position
    }

    void Update()
    {
        // Calculate the distance moved since the last afterimage was spawned
        if (SpeedBoostManager.speeedApplied == true)
        {


            float distanceMoved = Vector3.Distance(transformTarget.transform.position, lastPosition);

            // Always spawn afterimages based on distance between them

            if (distanceMoved >= spawnDistance)
            {
                if (disableMainRender)
                {
                    mainRenderObj.SetActive(false);
                }

                lastPosition = transformTarget.transform.position;

                GameObject duplicatedObject = Instantiate(objectToDuplicate);
                duplicatedObject.transform.SetParent(parentObject.transform, false); // Ensure consistent scale
                Transform cube = duplicatedObject.transform.GetChild(0); // 0 is the index of the first child

                cube.gameObject.SetActive(true); // Or false to deactivate

                if (transformTarget != null)
                {
                    duplicatedObject.transform.position = transformTarget.transform.position;
                    duplicatedObject.transform.rotation = transformTarget.transform.rotation;
                    // Set the scale of the duplicated object to match the original objectToDuplicate
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

                StartCoroutine(FadeAndDestroy(duplicatedObject, lifespan));
            }
            else
            {
                if (!PlayerManager.gameOver)
                {
                    mainRenderObj.SetActive(true);
                }
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

                // Ensure the scale does not change based on speed
                //root.transform.localScale = objectToDuplicate.transform.localScale; // Uncomment if needed

                if (applyRainbowEffect)
                {
                    hueShift += rainbowColorChangeSpeed * Time.deltaTime; // Control the speed of color change
                    if (hueShift > 1.0f) hueShift -= 1.0f;
                }
            }
        }
    }

    IEnumerator FadeAndDestroy(GameObject afterImage, float duration)
    {
        SkinnedMeshRenderer skinnedMeshRenderer = afterImage.transform.Find("Cube").GetComponent<SkinnedMeshRenderer>();
        Material material = skinnedMeshRenderer.material;

        Color initialColor = material.GetColor("_TintColor");
        float elapsedTime = 0.0f;
        float fadeRate = duration; // Remove the dependency on forwardSpeed for fading

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
