using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteEnemy : EnemyController
{
    private Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void Start()
    {
        base.Start();
        currentHealth = maxHealth;
        damage = 50;
        hit = 50;
    }

    void FixedUpdate()
    {
        Movement(rigidbody2D);

        if (transform.localPosition.x == -16 || transform.localPosition.x == 40)
        {
            direction = -direction;
            Flip();
        }
    }
}
