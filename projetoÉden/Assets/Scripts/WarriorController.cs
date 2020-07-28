using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <inheritdoc/>
/// <summary>
/// Main class that controls the avatar, his health, coins, level, powers, and sounds. 
/// </summary>
public class WarriorController : PlayerController
{
    /* Components */
    Rigidbody2D rigidbody2D;
    /// <value> Gets the component animator of warrior </value>
    public Animator playerAnimator {get {return animator; }}
    Animator animator;
    AudioSource audioSource;

    /* Control Coins */
    /// <value> Gets the current value of coins </value>
    public int coins { get { return quantCoins; } } //exemplo de get reduzido
    int quantCoins = 0;
    public Text numCoins;

    public GameObject chickenShoot;

    /// <value> Gets the current value of level </value>
    public static int level { get {return currentLevel;} }
    static int currentLevel; //to be accessed by the class coding screen

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    public override void Start()
    {
        base.Start();
        currentHealth = maxHealth;
        numCoins.text = quantCoins.ToString();
        currentLevel = 3;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Movement(rigidbody2D);
        CheckCollisionForJump(rigidbody2D, animator);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(move)); 

        numCoins.text = quantCoins.ToString();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack");
        }
    }

    /// <inheritdoc/>
    /// <remarks> Sets UI Health Bar at every change. </remarks>
    public override void ChangeHealth(int amount)
    {
        base.ChangeHealth(amount);

        UIHealthBar.instance.SetValue(health);
    }

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(groundCheck.position, range);
    }

    /// <summary>
    /// Updates the level by one more. 
    /// </summary>
    public void GoToNextLevel()
    {
        currentLevel++;
    }

    /// <summary>
    /// Adds one more coin. 
    /// </summary>
    public void AddCoin()
    {
        quantCoins+=1;
    }

    /// <summary>
    /// Launches a flying chicken from hands of warrior. 
    /// </summary>
    void Launch()
    {
        //creates a copy with no rotation, near of hands of warrior
        GameObject projectileObject = Instantiate(chickenShoot, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

        ChickenShoot projectile = projectileObject.GetComponent<ChickenShoot>();

        if (transform.localScale.x > 0)
            projectile.Launch(new Vector2(1, 0), 300);
        else //never it's zero
            projectile.Launch(new Vector2(-1, 0), 300);
    }

    /// <summary>
    /// Changes the rigidbody body type to static when the coding screen it's actived. 
    /// </summary>
    /// <param name = "state"> A bool, whether to deactivate or activate. </param>
    public void DeactivateMovement(bool state)
    {
        if (state)
            rigidbody2D.bodyType = RigidbodyType2D.Static;
        else
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    /// <summary>
    /// Changes the warrior height and width in the same proportion.
    /// </summary>
    /// <param name = "scale"> A float, the scale value to change health. </param>
    public void ChangeHeight(float scale)
    {
        transform.localScale = new Vector3(scale, scale, 0);
    }

    /// <summary>
    /// Plays a specific sound, normally when occurs collision.
    /// </summary>
    /// <param name = "clip"> The AudioClip to be executed. </param>
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
