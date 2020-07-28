using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    Fade fade;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        fade = gameObject.AddComponent<Fade>();
    }

    /// <summary>
    /// Puts out the flame, executing a fade out and later destrying the flame.
    /// </summary>
    public void PutOut() 
    {
        fade.StartFade(1); //fade out
    }

    public void Ignite()
    {
        //implement later
    }

    void OnTriggerStay2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null)
        {
            controller.ChangeHealth(-100);
        }
    }
}
