using UnityEngine;
using System.Collections;
public class FlexiblePositioning : MonoBehaviour
{
    RectTransform rectTransform;

    // Public variables to set anchors and pivot
    public Vector2 anchorMin = new Vector2(0.5f, 0.5f);
    public Vector2 anchorMax = new Vector2(0.5f, 0.5f);
    public Vector2 pivot = new Vector2(0.5f, 0.5f);

    // Public variables to set animation positions
    public Vector2 startPosition = Vector2.zero;
    public Vector2 endPosition = Vector2.zero;
    public float animationDuration = 2f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SetAnchorsAndPivot();
        StartAnimation();
    }

    void SetAnchorsAndPivot()
    {
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.pivot = pivot;
    }

    void StartAnimation()
    {
        StartCoroutine(AnimatePosition(startPosition, endPosition));
    }

    IEnumerator AnimatePosition(Vector2 from, Vector2 to)
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(from, to, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = to;
    }
}