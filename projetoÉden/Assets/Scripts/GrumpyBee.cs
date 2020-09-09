﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// </inheritdoc>
public class GrumpyBee : EnemyController
{
    private Rigidbody2D rigidbody2D;
    
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
        currentHealth = maxHealth;
        damage = 100;
        hit = 0; //only suffers damage when collide with a chicken
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (WarriorController.level == 2 && GameObject.FindWithTag("Chicken") == null)
        {
            MovementWithoutCollision(rigidbody2D, -1);
        }     
    }
}
