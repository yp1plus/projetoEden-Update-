using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    Renderer renderer;
    CanvasGroup rendererCanvas;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        rendererCanvas = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Executes the fade in when called.
    /// </summary>
    public void FadeIn()
    {
        if (renderer != null
            || rendererCanvas != null)
            StartCoroutine(EnumeratorFadeIn());
    }

    /// <summary>
    /// Executes the fade out when called.
    /// </summary>
    public void FadeOut()
    {
        if (renderer != null
            || rendererCanvas != null) 
            StartCoroutine(EnumeratorFadeOut());
    }

    IEnumerator EnumeratorFadeIn()
    {
        if (renderer != null)
            for (float f = 0; f <=1; f += 0.05f){
                Color color = renderer.material.color;
                color.a = f; //Alpha component of the color (0 is transparent, 1 is opaque)
                renderer.material.color = color;
                yield return new WaitForSeconds(0.05f);
            }
        
        else if (rendererCanvas != null)
            for (float f = 0; f <=1; f += 0.05f){
                rendererCanvas.alpha = f; //Alpha component of the color (0 is transparent, 1 is opaque)
                yield return new WaitForSeconds(0.01f);
            }
    }

    IEnumerator EnumeratorFadeOut()
    {
        if (renderer != null)
            for (float f = 1f; f >= 0; f -= 0.05f){
                Color color = renderer.material.color;
                color.a = f; //Alpha component of the color (0 is transparent, 1 is opaque)
                renderer.material.color = color;
                yield return new WaitForSeconds(0.05f);
            }

        else if (rendererCanvas != null)
            for (float f = 1f; f >= 0; f -= 0.05f){
                rendererCanvas.alpha = f; //Alpha component of the color (0 is transparent, 1 is opaque)
                yield return new WaitForSeconds(0.01f);
            }
    } 
}
