using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonHealthBar : MonoBehaviour
{
    /// <value> Gets the value of static instance of the class </value>
    public static DragonHealthBar instance { get; private set; }
    public Image mask;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    /// <summary>
    /// Reduces UIHealthBar size. 
    /// </summary>
    /// <param name = "value"> A int, the current health value. </param>
    public void SetValue(int value)
    {
        if (mask.rectTransform == null)
            return;
        
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (float) value/100);
    }

    public void ResetBar()
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize);
    }
}
