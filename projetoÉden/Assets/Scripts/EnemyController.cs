using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlayerController
{
    public int damage;
    public int hit;
    /* Time of Movement */
    public float changeTime;
    private float timer;

    int direction = 1;

    void Start()
    {
        mainRenderer = GetComponent<SpriteRenderer>();
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        
        if (player != null)
        {
            Debug.Log("I'm Here");
            if (!player.AnimatorIsPlaying("Attack", player.playerAnimator) && !player.AnimatorIsPlaying("Jump Attack", player.playerAnimator))
            {
                player.ChangeHealth(-damage);
            }
            else
            {
                ChangeHealth(-hit);   //Change this after
            } 
        } 
    }

    public override void Movement(Rigidbody2D rigidbody2D)
    {
        rigidbody2D.velocity = new Vector2(direction * speed, rigidbody2D.velocity.y);

        if (timer < 0) //if collide with block
        {
            direction = -direction;
            Flip();
        }
    }

    public override void ChangeHealth(int amount)
    {
        base.ChangeHealth(amount);

        if (health == 0)
        {
            //To Implement Fade Out later
            Destroy(gameObject);
        }

    }
}
