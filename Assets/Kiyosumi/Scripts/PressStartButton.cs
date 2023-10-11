using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressStartButton : MonoBehaviour
{
    [SerializeField] Text textIcon;
    [SerializeField] Text textString;
    [SerializeField, Range(0.0f, 10.0f)] float blinkingSpeed = 1.0f;

    float sinPal = 0.0f; // sinÇÃïœêî

    void Update()
    {
        sinPal += blinkingSpeed * Time.deltaTime;
        var alpha = (Mathf.Sin(sinPal) + 2.0f) / 3.0f;

        var iconColor = textIcon.color;
        iconColor.a = alpha;
        textIcon.color = iconColor;

        var stringColor = textIcon.color;
        stringColor.a = alpha;
        textString.color = stringColor;
    }
}
