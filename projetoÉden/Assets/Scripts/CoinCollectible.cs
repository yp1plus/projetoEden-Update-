using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null)
        {
                Destroy(gameObject);
                controller.AddCoin();
                controller.PlaySound(collectedClip);
        }
    }
}
