using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipScene : MonoBehaviour
{
    float lerpLength = 1f;

    Color fadeOutColor = new Color(1, 1, 1, 0);
    Color fadeInColor = new Color(1, 1, 1, 1);

    Color initialColor, finalColor;
    Text text;
    [SerializeField] AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = fadeInColor;
        finalColor = fadeOutColor;

        text = GetComponent<Text>();
        StartCoroutine(ColorLerp(initialColor, finalColor, lerpLength, curve));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ColorLerp(Color start, Color end, float lerpLength, AnimationCurve curve)
    {
        float timeElapsed = 0f;

        while (timeElapsed < lerpLength) 
        {
            text.color = Color.Lerp(start, end, curve.Evaluate(timeElapsed / lerpLength));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        text.color = end;
        SwitchTarget();
        StartCoroutine(ColorLerp(initialColor, finalColor, lerpLength, curve));

    }

    void SwitchTarget()
    {
        initialColor = initialColor == fadeInColor ? fadeOutColor : fadeInColor;
        finalColor = finalColor == fadeOutColor ? fadeInColor : fadeOutColor;
    }
}
