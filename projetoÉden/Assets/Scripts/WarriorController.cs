using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorController : PlayerController
{
    /* Components */
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    public Animator playerAnimator {get {return animator; }}
    private AudioSource audioSource;

    /* Control Coins */
    public int coins { get { return quantCoins; } } //exemplo de get reduzido
    private int quantCoins = 0;
    public Text numCoins;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        numCoins.text = quantCoins.ToString();
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        //rigidbody2D.velocity = new Vector2(moveInput * speed, rigidbody2D.velocity.y);
        Movement(rigidbody2D);

        animator.SetFloat("Speed", Mathf.Abs(move));

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Attack");
        }
        
        CheckCollisionForJump(rigidbody2D, animator);
        BetterJumping(rigidbody2D);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        numCoins.text = quantCoins.ToString();
    }

    public override void ChangeHealth(int amount)
    {
        base.ChangeHealth(amount);

        UIHealthBar.instance.SetValue(health);
        Debug.Log(health);
    }

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(groundCheck.position, range);
    }

    public void AddCoin()
    {
        //está adicionando duas vezes
        quantCoins+=1;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
