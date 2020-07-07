using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Executes the fade in or out of player when called.
    /// </summary>
    /// </param name = "type"> A int, the type (0 -> FadeIn and 1 -> FadeOut) of fade. </param>
     /// </param name = "component"> The Renderer Object to fade. </param>
    public void StartFade(int type)
    {
        if (type == 0)
            StartCoroutine(FadeIn());
        else if (type == 1)
            StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <=1; f += 0.05f){
            Color color = renderer.material.color;
            color.a = f; //Alpha component of the color (0 is transparent, 1 is opaque)
            renderer.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0; f -= 0.05f){
            Color color = renderer.material.color;
            color.a = f; //Alpha component of the color (0 is transparent, 1 is opaque)
            renderer.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(gameObject);
    } 
}
