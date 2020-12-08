using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : EnemyController
{
    private Rigidbody2D rigidbody2D;
    Animator animator;
    Renderer renderer;
    Collider2D collider;
    bool isVisible;
    bool playerHit = false;
    public GameObject enemies;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider2D>();
    }
    
    ///</inheritdoc>
    public override void Start()
    {
        base.Start();
        speed = 13;
        currentHealth = maxHealth;
        damage = 100;
        hit = 0; //doesn't suffer hit
        isVisible = false;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (isVisible && transform.position.x < 632)
        {
            MovementWithoutCollision(rigidbody2D, 1);
        }
        else if (transform.position.x >= 632)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            animator.enabled = false;
        }

        if(!playerHit)
            speed = WarriorController.instance.speed + 3;
        else
            DecreaseSpeed();
    }

    public void DecreaseSpeed()
    {
        playerHit = true;
        speed = WarriorController.instance.speed - 3;
    }

    public void MakeVisible()
    {
        isVisible = true;
        renderer.enabled = true;
        collider.enabled = true;
        animator.enabled = true;
        enemies.SetActive(true);
    }
}
