using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the collision with a coin, updating the number of coins and playing a sound.
/// </summary>
public class CoinCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    bool flag = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null && !flag)
        {
            flag = true;
            controller.AddCoin();
            Destroy(gameObject);
            controller.audioController.PlaySound(collectedClip);
        }
    }
}
