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
    CapsuleCollider2D capsuleCollider2D;
    bool activated = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null && !activated)
        {
            if ((controller.GetPosition().x >= 180 && WarriorController.level < (int) WarriorController.PHASES.BATTLE)
               || (controller.GetPosition().x >= 518 && WarriorController.level < (int) WarriorController.PHASES.FIRST_OF_STRUCTURES)
               || (controller.GetPosition().x >= 560 && WarriorController.level < (int) WarriorController.PHASES.FLOATING_PLATFORM)
               || (controller.GetPosition().x >= 891.8f && WarriorController.level < (int) WarriorController.PHASES.FOURTH_WALL))
            {
                controller.GoToNextLevel();
            }
            if (WarriorController.level == (int) WarriorController.PHASES.BLADES_BARRIER)
                MainMenu.StartScene((int) WarriorController.PHASES.LAST_OF_STRUCTURES);
            if (controller.GetPosition().x >= 518 && WarriorController.level < (int) WarriorController.PHASES.FLOATING_PLATFORM)
                UIController.instance.ShowNewInfo(6);
            else if (controller.GetPosition().x >= 891.8f)
                UIController.instance.ShowNewInfo(7);
            if (!MainMenu.SceneIsLoaded(WarriorController.level))
                MainMenu.StartScene(WarriorController.level);
            activated = true;
            MainMenu.lastCheckPointPosition = transform.position;
            MainMenu.lastLevel = WarriorController.level;
            MainMenu.quantCoins = controller.coins;
            if (animator != null)
                animator.SetTrigger("Activated");
            capsuleCollider2D.enabled = false;
        }
    }
}
