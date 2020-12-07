using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : EnemyController
{
    public static DragonController instance { get; private set; }
    float timeMaxAttack = 0.9f;
    float time = 0;
    Animator animator;
    AudioController audioController;
    Rigidbody2D rigidbody2D;
    bool isBurning = false;
    public AudioClip audioScream;
    bool actived = false;
    public GameObject portal;

    void Awake()
    {
        instance = this;
        audioController = gameObject.AddComponent<AudioController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WarriorController.instance.IsFinalBattle() || actived)
        {
            actived = true;

            if (AnimatorIsPlaying("Dragon_Attack", animator) && !isBurning)
                StartCoroutine(CheckFlame());

            time += Time.deltaTime;
            if (time > timeMaxAttack)
            {
                AnimateAttack();
                time = 0;
                timeMaxAttack = Random.Range(2f, 7f);
            }
        }
    }

    IEnumerator CheckFlame()
    {
        yield return new WaitForSeconds(0.3f);
        if (AnimatorIsPlaying("Dragon_Attack", animator) && !isBurning)
            WarriorController.instance.ChangeHealth(-100);
    }

    IEnumerator ResetIsBurning()
    {
        yield return new WaitForSeconds(2f);
        isBurning = false;
    }

    void AnimateAttack()
    {
        if (!AnimatorIsPlaying("Dragon_Attack", animator) && !audioController.audioIsPlaying)
        {
            animator.SetTrigger("Attack");
            audioController.PlaySound(audioScream);
        }
    }

    void OnDestroy()
    {
        if (portal != null)
            portal.SetActive(true);
    }

    public override void ChangeHealth(int amount)
    {
        if (health == 0)
            return;

        if (amount < 0) //want to take life
        {
            if (isInvincible)
                return;

            isInvincible = true;

            StartCoroutine(WaitResetInvincibility());
        }
        
        //The first value it is between the second and third
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        DragonHealthBar.instance.SetValue(health);
        
        if (health == 0)
            DragonHealthBar.instance.Destroy();
        
        DestroyPlayerDead();
    }

    IEnumerator WaitResetInvincibility()
    {
        yield return new WaitForSeconds(1);
        ResetInvincibility();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        FlameController controller = other.GetComponent<FlameController>();
        ChickenShoot e = other.gameObject.GetComponent<ChickenShoot>();

        if (player != null)
        {
            Attack(player);
        }
        else if (controller != null)
        {
            isBurning = true;
            StartCoroutine(ResetIsBurning());
        } else if (e != null)
        {
            e.DestroyGameObject();

            if (!AnimatorIsPlaying("Dragon_Attack", animator))
            {
                animator.SetTrigger("Attacked");
                ChangeHealth(-100);
            }
        }
    }

    protected override void Attack(WarriorController player)
    {
        if (!PlayerController.AnimatorIsPlaying("Attack", player.playerAnimator) 
            && !PlayerController.AnimatorIsPlaying("Jump Attack", player.playerAnimator))
        {
            if (!invincible)
            {
                player.ChangeHealth(-damage);
            }   
        }
        else
        {
            if (!AnimatorIsPlaying("Dragon_Attack", animator))
            {
                animator.SetTrigger("Attacked");
                if (WarriorController.instance.GetScale().x >= 2)
                    ChangeHealth(-(int)(hit*1.5f));
                else if (WarriorController.instance.GetScale().x >= 4)
                    ChangeHealth(-hit*2);
                else
                    ChangeHealth(-hit);
            }
        } 
    }
}
