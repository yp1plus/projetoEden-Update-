using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : EnemyController
{
    Rigidbody2D rigidbody2D;
    Vector3 targetPosition;
    Animator animator;
    Vector3 previousPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (WarriorController.instance.barrierActivated && UIController.instance.powers[3].activeSelf)
        {
            targetPosition = (WarriorController.instance.GetPosition() - transform.position).normalized;

            float direction = transform.position.x - previousPosition.x;
            previousPosition = transform.position;

            if (direction > 0 && !facingRight || direction < 0 && facingRight)
            {
                Flip();
            }

            if (Vector2.Distance(transform.position, WarriorController.instance.GetPosition()) > 5)
            {
                animator.SetFloat("Speed", 1f);
                animator.SetBool("Attack", false);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(WarriorController.instance.GetPosition().x, transform.position.y), speed * Time.deltaTime);
            } 
            else
            {
                animator.SetFloat("Speed", 0);
                animator.SetBool("Attack", true);
            }
        }
    }
}
