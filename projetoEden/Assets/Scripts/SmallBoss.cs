using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBoss : EnemyController
{
    Rigidbody2D rigidbody2D;
    Vector3 targetPosition;
    Animator animator;
    Vector3 previousPosition;
    float timeMaxAttack = 0.9f;
    float time = 0;

    public static SmallBoss instance {get; private set;}

    void Awake()
    {
        instance = this;
    }

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
        if (GameObject.FindGameObjectWithTag("BlueEnemy") == null)
        {
            targetPosition = (WarriorController.instance.GetPosition() - transform.position).normalized;

            float direction = transform.position.x - previousPosition.x;
            previousPosition = transform.position;

            if (direction > 0 && !facingRight || direction < 0 && facingRight)
            {
                Flip();
            }

            if (Vector2.Distance(transform.position, WarriorController.instance.GetPosition()) > 9)
            {
                animator.SetFloat("Speed", 1f);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(WarriorController.instance.GetPosition().x, transform.position.y), speed * Time.deltaTime);
            } 
            else
            {
                time += Time.deltaTime;
                animator.SetFloat("Speed", 0);
                if (time > timeMaxAttack)
                {
                    AnimateAttack();
                    time = 0;
                    timeMaxAttack = Random.value;
                }
                    
            }
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        BarrierController.instance.ResetAttributes();
        if (!MainMenu.SceneIsLoaded(3))
            MainMenu.StartScene(3);
    }

    void AnimateAttack()
    {
        if (Random.value >= 0.5 && !AnimatorIsPlaying("Attack2", animator))
            animator.SetTrigger("Attack");
        else if (!AnimatorIsPlaying("Attack", animator))
        {
            animator.SetTrigger("Attack2");
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        FlameController controller = other.gameObject.GetComponent<FlameController>();
        ChickenShoot e = other.gameObject.GetComponent<ChickenShoot>();
        
        if (player != null)
        {
            Attack(player);
        }
        else if (controller != null)
        {
            ChangeHealth(-hitFlame);
        } else if (e != null)
        {
            e.DestroyGameObject();

            if (!AnimatorIsPlaying("SmallBoss_Attack1", animator) && !AnimatorIsPlaying("SmallBoss_Attack2", animator))
            {
                ChangeHealth(-hitChicken);
            }
        }
        
        if (player != null)
            animator.SetTrigger("Attack2");
    }

    protected override void Attack(WarriorController player)
    {
        if (!PlayerController.AnimatorIsPlaying("Attack", player.playerAnimator) 
            && !PlayerController.AnimatorIsPlaying("Jump Attack", player.playerAnimator))
        {
            if (!invincible)
            {
                if (AnimatorIsPlaying("SmallBoss_Attack1", animator) || AnimatorIsPlaying("SmallBoss_Attack2", animator))
                    player.ChangeHealth(-damage);
                else
                    player.ChangeHealth(-damage/4);
            }   
        }
        else
        {
            if (AnimatorIsPlaying("SmallBoss_Attack1", animator) || AnimatorIsPlaying("SmallBoss_Attack2", animator))
            {
                if (!invincible)
                    player.ChangeHealth(-damage/2);
            }
            else
            {
                ChangeHealth(-hit);
            }
        } 
    }
}
