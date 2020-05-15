using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }
    public Image[] mask = new Image[4];
    float originalSize;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        originalSize = mask[0].rectTransform.rect.width; //todos têm o mesmo tamanho
    }

    public void SetValue(float value)
    {
        if (value >= 75 && value <= 100)
        {
            value -= 75;
            mask[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value/25);
        } 
        else if (value >= 50 && value < 75) 
        {
            value -= 50;
            mask[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[1].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value/25);
        } 
        else if (value >= 25 && value < 50) 
        {
            value -= 25;
            mask[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[1].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[2].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value/25);
        }
        else if (value >= 0 && value < 25)
        {
            mask[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[1].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[2].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            mask[3].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value/25);
        }
    }
}
