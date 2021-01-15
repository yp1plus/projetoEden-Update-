using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// </inheritdoc>
public class GrumpyBee : EnemyController
{
    private Rigidbody2D rigidbody2D;
    private bool movementActivated = false;
    public bool activated = true;
    
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
        invincibleTimer = 1.0f;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (!movementActivated && activated && WarriorController.level == 1 && GameObject.FindWithTag("Chicken") == null)
        {
            if (GameObject.FindWithTag("GrumpyBee") != null)
                UIController.instance.ShowNewInfo();
            movementActivated = true;
        }

        if (!activated && GameObject.FindWithTag("GrumpyBee") == null)
        {
            activated = true;
        }

        if (movementActivated && !UIController.instance.InfoIsActive())
            MovementWithoutCollision(rigidbody2D, -1);
        
        if (currentHealth == 0)
            rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
}
