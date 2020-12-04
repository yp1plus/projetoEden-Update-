using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the collision with the blue ball, opening a new mission and updating the level.
/// </summary>
public class CheckPoint : MonoBehaviour
{
    Animator animator;
    bool activated = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null && !activated)
        {
            activated = true;
            MainMenu.lastCheckPointPosition = transform.position;
            MainMenu.lastLevel = WarriorController.level;
            if (animator != null)
                animator.SetTrigger("Activated");
        }
    }
}
