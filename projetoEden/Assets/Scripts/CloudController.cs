using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : EnemyController
{
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        
        if (player != null)
        {
            if(player.GetScale().x >= 2.5f)
            {
                ChangeHealth(-100);
            }
        } 
    }
}
