using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLine : MonoBehaviour 
{
    [SerializeField]
    public float animTime = 0;

    [SerializeField]
    public float maxWidthStraight = 0;

    [SerializeField]
    public float maxWidthDiag = 0;

    private float startWidth;

    private float maxWidth;

    private RectTransform rectTransform;

    private void Awake()
    {
        animTime = Mathf.Max(animTime, 0.1f);

        rectTransform = GetComponent<RectTransform>();

        startWidth = rectTransform.sizeDelta.x;

        maxWidth = maxWidthStraight;
    }

    private void OnEnable()
    {
        StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        while(rectTransform.sizeDelta.x < maxWidth)
        {
            float scaleSpeed = (maxWidth - startWidth) / animTime;

            rectTransform.sizeDelta += new Vector2(scaleSpeed * Time.deltaTime, 0f);

            if (rectTransform.sizeDelta.x > maxWidth)
            {
                rectTransform.sizeDelta = new Vector2(maxWidth, rectTransform.sizeDelta.y);
            }
                
            yield return null;
        }
    }

    /// <summary>
    /// Changes maxWidth of the line for straight or diagonal conditions.
    /// </summary>
    /// <param name="straight">If true - line is straight, if false - line is diagonal.</param>
    public void SetStraight(bool straight)
    {
        if (straight)
        {
            maxWidth = maxWidthStraight;
        }
        else
        {
            maxWidth = maxWidthDiag;
        }
    }

    public void ResetWinLine ()
    {
        rectTransform.sizeDelta = new Vector2(startWidth, rectTransform.sizeDelta.y);
    }
}
