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
            Attack(player);
        } 
        else if (other.gameObject.tag == "Corner") 
        {
            isInCorner = true;
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        FlameController controller = other.GetComponent<FlameController>();
        
        if (player != null)
        {
            Attack(player);
        }
        else if (controller != null)
        {

            ChangeHealth(-100);
            DestroyPlayerDead();
        }
    }

    void Attack(WarriorController player)
    {
        if (!player.AnimatorIsPlaying("Attack", player.playerAnimator) 
            && !player.AnimatorIsPlaying("Jump Attack", player.playerAnimator))
        {
            if (!invincible)
                player.ChangeHealth(-damage);
        }
        else
        {
            ChangeHealth(-hit);
            DestroyPlayerDead();
        } 
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        
        if (player != null)
        {
            Attack(player);
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
    /// <param name = "direction"> A integer, the movement direction. </param>
    public void MovementWithoutCollision(Rigidbody2D rigidbody2D, int direction)
    {
        rigidbody2D.velocity = new Vector2(direction * speed, rigidbody2D.velocity.y);
    }
}
