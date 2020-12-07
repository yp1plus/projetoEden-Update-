using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();

        if (player != null && Mathf.Approximately(player.move, 0))
        {
            player.SetPosition(transform.position.x);
        }
    }
}
