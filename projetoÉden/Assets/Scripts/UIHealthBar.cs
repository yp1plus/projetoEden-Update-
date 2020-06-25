using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI Health Bar, reducing each heart and destroy it every 25 lost units of health.
/// </summary>
public class UIHealthBar : MonoBehaviour
{
    /// <value> Gets the value of static instance of the class </value>
    public static UIHealthBar instance { get; private set; }
    public Image[] mask = new Image[4];
    float originalSize;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        originalSize = mask[0].rectTransform.rect.width; //all have the same size
    }

    /// <summary>
    /// Reduces each heart size, destroying it every 25 lost units of health. 
    /// </summary>
    /// <param name = "value"> A int, the current health value. </param>
    public void SetValue(int value)
    {
        if (value >= 75 && value <= 100)
        {
            value -= 75;
            mask[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (float) value/25);
        } 
        else if (value >= 50 && value < 75) 
        {
            value -= 50;
            mask[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[1].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (float) value/25);
        } 
        else if (value >= 25 && value < 50) 
        {
            value -= 25;
            mask[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[1].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[2].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (float) value/25);
        }
        else if (value >= 0 && value < 25)
        {
            mask[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[1].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[2].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[3].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (float) value/25);
        }
    }
}
