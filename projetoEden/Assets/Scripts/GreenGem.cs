using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGem : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null)
        {
            controller.ChangeHealth(100);
            Destroy(gameObject);
            controller.audioController.PlaySound(collectedClip);
        }
    }
}
