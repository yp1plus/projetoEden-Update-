﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : EnemyController
{
    private Rigidbody2D rigidbody2D;
    bool isVisible;
    bool playerHit = false;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        if (isVisible)
        {
            MovementWithoutCollision(rigidbody2D, 1);
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

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void MakeVisible()
    {
        isVisible = true;
        //gameObject.SetActive(true);
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;
    }
}