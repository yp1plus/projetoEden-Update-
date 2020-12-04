using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public AudioController audioController {get; private set;}

    /* Control Coins */
    /// <value> Gets the current value of coins </value>
    public int coins { get { return quantCoins; } } //exemplo de get reduzido
    int quantCoins = 100;
    public TMP_Text numCoins;

    public GameObject chickenShoot;

    /// <value> Gets the current value of level </value>
    public static int level { get {return currentLevel;} }
    static int currentLevel; //to be accessed by the class coding screen

    static bool canDeactivateStone;

    public static bool StoneDeactivated { get => canDeactivateStone; set => canDeactivateStone = value;} 

    public static WarriorController instance { get; private set; }

    public GameObject flame;

    
    const float DEFAULT_HEIGHT = 119.5f;
    float height = DEFAULT_HEIGHT;

    const float POSITION_NEXT_TO_DRAGON = 827.3f;

    public static bool isSubPhase = false;
    int quantChickens = 0;
    public TMP_Text txtNumChickens;
    public TMP_Text txtFlameIsBurning;
    public TMP_Text txtWarriorHeight;
    public AudioClip[] attackSongs = new AudioClip[4];
    public AudioClip attacked;
    public AudioClip darkAmbient;
    System.Random random = new System.Random();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioController = gameObject.AddComponent<AudioController>();
        instance = this;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    public override void Start()
    {
        base.Start();
        numCoins.text = quantCoins.ToString();
        currentLevel = MainMenu.lastLevel;
        canDeactivateStone = false;
    }

    public void LoadFlame(GameObject flame)
    {
        this.flame = flame;
    }

    public void ResetWarriorAttributes()
    {
        currentLevel = MainMenu.lastLevel;
        canDeactivateStone = false;
        transform.position = MainMenu.lastCheckPointPosition;
        currentHealth = 100;
        ChangeHeight(facingRight ? 1: -1);
        ResetInvincibility();
        if (!facingRight)
            Flip();
        UIHealthBar.instance.ResetBar();
        StartCoroutine(DelayReset());
    }

    IEnumerator DelayReset()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (rigidbody2D.bodyType == RigidbodyType2D.Dynamic)
        {
            Movement(rigidbody2D);
            CheckCollisionForJump(rigidbody2D, animator);
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(move)); 

        numCoins.text = quantCoins.ToString();
        txtNumChickens.text = quantChickens.ToString();
        if (flame != null)
            txtFlameIsBurning.text = flame.GetComponent<FlameController>().isBurning.ToString().ToLower();
        txtWarriorHeight.text = height.ToString("F1").Replace(',','.');

        if (!CodingScreen.instance.panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.C) && UIController.instance.powers[0].activeSelf)
            {
                Launch();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                animator.SetTrigger("Attack");
                audioController.PlaySound(attackSongs[(int) random.Next(0, 3)]) ;
            }

            if (Input.GetKeyDown(KeyCode.E) && UIController.instance.powers[1].activeSelf)
            {
                if (flame != null && flame.GetComponent<Renderer>().material.color.a <= 0.05f)
                    flame.GetComponent<FlameController>().Ignite();
            }

            if (Input.GetKeyDown(KeyCode.Q) && UIController.instance.powers[2].activeSelf)
            {
                ChangeHeight(transform.localScale.x*1.2f);
            }

            if (Input.GetKeyDown(KeyCode.Z) && UIController.instance.powers[2].activeSelf)
            {
                ChangeHeight(transform.localScale.x/1.2f);
            }

            if (transform.position.x >= 809f && currentLevel < 10)
                GoToNextLevel();
        }
    }

    /// <inheritdoc/>
    /// <remarks> Sets UI Health Bar at every change. </remarks>
    public override void ChangeHealth(int amount)
    {
        if (amount < 0 && !isInvincible)
            audioController.PlaySound(attacked);
            
        base.ChangeHealth(amount);

        //DestroyPlayerDead();

        UIHealthBar.instance.SetValue(health);
        
        if (health == 0)
             CodingScreen.instance.ShowGameOver(true);
    }

    public void SubtractChicken()
    {
        if (quantChickens == 0)
            return;
        
        quantChickens--;
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
        if (currentLevel != 5 || isSubPhase) //The level 5 cointains one subphase
        {
            currentLevel++;
        }
        else
        {
            isSubPhase = true;
        }  

        if (currentLevel == 10)
            audioController.PlayMusic(darkAmbient);
    }

    /// <summary>
    /// Adds one more coin. 
    /// </summary>
    public void AddCoin()
    {
        quantCoins++;
    }
    
    /// <summary>
    /// Removes coins if there is enough amount of coins. 
    /// </summary>
    /// <param name = "quant"> A int, the quantity of coins. </param>
    /// <returns> A bool, if there is coins enough </return>
    public bool RemoveCoins(int quant)
    {
        if (quantCoins - quant >= 0)
        {
            quantCoins -= quant;
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    void OnBecameInvisible()
    {
        if (gameObject != null && gameObject.GetComponent<Renderer>().enabled) //avoids to destroy the player when him are flashing 
        {
            ChangeHealth(-100);
        }
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
            projectile.Launch(new Vector2(1, 0), 400);
        else //never it's zero
            projectile.Launch(new Vector2(-1, 0), 400);
        
        quantChickens++;
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
        float changeFactor = DEFAULT_HEIGHT/9;

        if (Mathf.Abs(transform.localScale.x) < Mathf.Abs(scale))
        {
            range = new Vector3(range.x * 1.2f, range.y * 1.2f, 0);
            speed = Mathf.Clamp(speed - 1f, 2, 20);
            if (jumpForce >= 0 && jumpForce <= 7)
                jumpForce = Mathf.Clamp(jumpForce - 1f, 0, 14);
            else
                jumpForce = Mathf.Clamp(jumpForce - 2f, 0, 14);
        }
        else
        {
            range = new Vector3(range.x / 1.2f, range.y / 1.2f, 0);
            speed += 1f; 
            if (jumpForce >= 0 && jumpForce < 7)
                jumpForce += 1f;
            else
                jumpForce += 2f;
        } 
            
        
        if (scale == 1)
        {
            range = new Vector3(-3, 0.3f, 0);
            jumpForce = 7;
            speed = 10;
        }
           

        transform.localScale = new Vector3(scale, Mathf.Abs(scale), 0);

        height = (Mathf.Abs(scale) == 1) ? DEFAULT_HEIGHT : Mathf.Abs(scale) * 100;

        if (transform.position.x < -37f || transform.position.y > 16.6) //change later
        {
            ChangeHealth(-100);
        }
    }

    public bool IsFinalBattle()
    {
        if (transform.position.x >= POSITION_NEXT_TO_DRAGON)
            return true;
        
        return false;
    }

    /// <summary>
    /// Sets the current player position x.
    /// </summary>
    /// <param name = "position"> A Vector3, the new position. </param>
    public void SetPosition(float position)
    {
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
    }
}
