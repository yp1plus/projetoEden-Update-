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

            StartCoroutine(CheckFlame());
        
            time += Time.deltaTime;
            if (time > timeMaxAttack)
            {
                AnimateAttack();
                time = 0;
                timeMaxAttack = Random.Range(1f, 7f);
            }
        }
    }

    IEnumerator CheckFlame()
    {
        yield return new WaitForSeconds(0.3f);
        //if (!WarriorController.instance.flame.GetComponent<FlameController>().isBurning && AnimatorIsPlaying("Dragon_Attack", animator))
            //WarriorController.instance.ChangeHealth(-100);
    }

    void AnimateAttack()
    {
        if (!AnimatorIsPlaying("Dragon_Attack", animator) && !audioController.audioIsPlaying)
        {
            animator.SetTrigger("Attack");
            audioController.PlaySound(audioScream);
        }
    }

    public override void ChangeHealth(int amount)
    {
        base.ChangeHealth(amount);

        DragonHealthBar.instance.SetValue(health);
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

        } else if (e != null)
        {
            e.DestroyGameObject();

            if (!AnimatorIsPlaying("Dragon_Attack", animator))
            {
                animator.SetTrigger("Attacked");
                ChangeHealth(-2);
            }
        }
    }

    protected override void Attack(WarriorController player)
    {
        if (!player.AnimatorIsPlaying("Attack", player.playerAnimator) 
            && !player.AnimatorIsPlaying("Jump Attack", player.playerAnimator))
        {
            if (!invincible)
            {
                player.ChangeHealth(-damage);
            }   
        }
        else
        {
            if (AnimatorIsPlaying("Dragon_Attack", animator))
            {
                if (!invincible)
                    player.ChangeHealth(-damage/2);
            }
            else
            {
                animator.SetTrigger("Attacked");
                ChangeHealth(-hit);
            }
        } 
    }
}
