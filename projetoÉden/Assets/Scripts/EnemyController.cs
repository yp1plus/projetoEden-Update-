using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <inheritdoc/>
/// <summary>
/// Controls the collision with the warrior, causing damage on enemy or warrior, and alternatives movements
/// </summary>
public class EnemyController : PlayerController
{
    public int damage; //to attack warrior
    public int hit; //to attack enemy

    /* Time of Movement */
    public float changeTime;
    float timer;

    bool isInCorner = false;

    int direction = -1;

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider (2D physics only).
    /// </summary>
    /// <remarks> If the warrior is attacking, causes damage on enemy, else causes damage on warrior.</remarks>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        
        if (player != null)
        {
            if (!player.AnimatorIsPlaying("Attack", player.playerAnimator) 
            && !player.AnimatorIsPlaying("Jump Attack", player.playerAnimator))
            {
                player.ChangeHealth(-damage);
            }
            else
            {
                ChangeHealth(-hit); //Change this after
            } 
        } 
        else if (other.gameObject.tag == "Corner") 
        {
            isInCorner = true;
        }
    }

    /// <summary>
    /// Controls the movement of enemie, which moves all the time, flipping when collides with block.
    /// </summary>
    /// <param name = "rigidbody2D"> The component Rigidbody2D of player. </param>
    public override void Movement(Rigidbody2D rigidbody2D)
    {
        rigidbody2D.velocity = new Vector2(direction * speed, rigidbody2D.velocity.y);

        if (isInCorner) //if collide with block
        {
            direction = -direction;
            Flip();
            isInCorner = false;
        }
    }

    /// <summary>
    /// Controls the movement of enemie, which moves always to the left.
    /// </summary>
    /// <param name = "rigidbody2D"> The component Rigidbody2D of player. </param>
    public void MovementWithoutCollision(Rigidbody2D rigidbody2D)
    {
        rigidbody2D.velocity = new Vector2(-direction * speed, rigidbody2D.velocity.y);
    }
}
