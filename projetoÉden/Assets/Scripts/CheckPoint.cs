using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the collision with the blue ball, opening a new mission and updating the level.
/// </summary>
public class CheckPoint : MonoBehaviour
{
    bool triggered;

    void Awake()
    {
        triggered = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null && !triggered)
        {
            triggered = true;
            controller.GoToNextLevel();
            CodingScreen.instance.OpenPanel(true);
            Destroy(gameObject); 
        }
    }
}
