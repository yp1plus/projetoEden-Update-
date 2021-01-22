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
    int quantCoins = 0;
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
    public static float height {get; private set;} = DEFAULT_HEIGHT;
    public static string name {get; private set;}

    const float POSITION_NEXT_TO_DRAGON = 934.9f;

    public static bool isSubPhase = MainMenu.isSubPhase;
    public static int quantChickens { get; private set; } = 0;
    public TMP_Text txtNumChickens;
    public TMP_Text txtFlameIsBurning;
    public TMP_Text txtWarriorHeight;
    public AudioClip[] attackSongs = new AudioClip[4];
    public AudioClip attacked;
    public AudioClip darkAmbient;
    System.Random random = new System.Random();
    bool flag = false;
    public enum PHASES {FIRST_OF_VARIABLES = 0, CHICKENS = 1, FLAME = 2, BATTLE = 3, BARRIER = 4, CLOUDS = 5, LAST_OF_VARIABLES = 5, CAMERAS = 6, FIRST_OF_STRUCTURES = 7, FLOATING_PLATFORM = 8, BUG = 9, FIRST_OF_ITERATIVE = 10, EMPTY_PATH = 10, BLADES_BARRIER = 11, LAST_OF_STRUCTURES = 12, FOURTH_WALL = 12, DRAGON = 13, END_MISSION = 14};

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
        if (numCoins != null)
            numCoins.text = quantCoins.ToString();
        currentLevel = MainMenu.lastLevel;
        isSubPhase = MainMenu.isSubPhase;
        canDeactivateStone = false;
    }

    public void SetName(string playerName)
    {
        TMP_Text nameTxt = GameObject.FindGameObjectWithTag("Name").GetComponent<TMP_Text>();
        nameTxt.text = playerName;
        name = playerName;
    }

    public void LoadFlame(GameObject flame)
    {
        this.flame = flame;
    }

    public void ResetWarriorAttributes()
    {
        if (currentLevel != 3)
            currentLevel = MainMenu.lastLevel;
        canDeactivateStone = false;
        transform.position = MainMenu.lastCheckPointPosition;
        currentHealth = 100;
        ResetHeight();
        ResetInvincibility();
        if (!facingRight)
            Flip();
        UIHealthBar.instance.ResetBar();
        DragonHealthBar.instance.ResetBar();
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

        if (numCoins != null)
            numCoins.text = quantCoins.ToString();
        if (txtNumChickens != null)
            txtNumChickens.text = quantChickens.ToString();
        if (flame != null)
            txtFlameIsBurning.text = flame.GetComponent<FlameController>().isBurning.ToString().ToLower();
        if (txtWarriorHeight != null)
            txtWarriorHeight.text = height.ToString("F1").Replace(',','.');

        if ((CodingScreen.instance == null || (!CodingScreen.instance.panel.activeSelf && !CodingScreen.instance.panelGameOver.activeSelf))
            && (EndMission.instance == null || (!EndMission.instance.panel.activeSelf && !EndMission.instance.panelGameWin.activeSelf))
            && Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack");
            audioController.PlaySound(attackSongs[(int) random.Next(0, 3)]) ;
        }

        if (CodingScreen.instance != null && !CodingScreen.instance.panel.activeSelf && !CodingScreen.instance.panelGameOver.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.C) && UIController.instance.powers[0].activeSelf)
            {
                Launch();
            }

            if (Input.GetKeyDown(KeyCode.E) && UIController.instance.powers[1].activeSelf)
            {
                if (flame != null && flame.GetComponent<Renderer>().material.color.a <= 0.05f)
                    flame.GetComponent<FlameController>().Ignite();
            }

            if (Input.GetKeyDown(KeyCode.Q) && UIController.instance.powers[3].activeSelf)
            {
                ChangeHeight(true, 1);
            }

            if (Input.GetKeyDown(KeyCode.Z) && UIController.instance.powers[3].activeSelf)
            {
                ChangeHeight(false, 1);
            }

            if (transform.position.x >= POSITION_NEXT_TO_DRAGON && currentLevel < (int) PHASES.DRAGON)
            {
                currentLevel = (int) PHASES.DRAGON;
                audioController.PlayMusic(darkAmbient);
            }
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

        if (UIHealthBar.instance != null)
            UIHealthBar.instance.SetValue(health);
        
        if (health == 0)
            if (CodingScreen.instance != null)
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
        if (currentLevel != (int) PHASES.CAMERAS || isSubPhase) //The level 5 cointains one subphase
        {
            currentLevel++;
        }
        else
        {
            isSubPhase = true;
        }  

        if (currentLevel == (int) PHASES.DRAGON)
            audioController.PlayMusic(darkAmbient);
    }

    /// <summary>
    /// Adds one more coin. 
    /// </summary>
    public void AddCoin()
    {
        quantCoins++;
    }

    public bool HaveCoinsEnough(int quant)
    {
        return quantCoins - quant >= 0;
    }
    
    /// <summary>
    /// Removes coins if there is enough amount of coins. 
    /// </summary>
    /// <param name = "quant"> A int, the quantity of coins. </param>
    /// <returns> A bool, if there is coins enough </return>
    public bool RemoveCoins(int quant)
    {
        if (HaveCoinsEnough(quant))
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
            projectile.Launch(new Vector2(1, 0), 800);
        else //never it's zero
            projectile.Launch(new Vector2(-1, 0), 800);
        
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
    /// <param name = "isIncrement"> A bool, if wants increment or decrement. </param>
    /// <param name = "times"> A unsigned int, the scale value to change health. </param>
    public void ChangeHeight(bool isIncrement, uint times)
    {
        float pow = Mathf.Pow(1.2f, times);
        float scale = isIncrement ? transform.localScale.x * pow : transform.localScale.x / pow;

        if (Mathf.Approximately(Mathf.Abs(scale), 1))
        {
            ResetHeight();
            return;
        } 
        else if (Mathf.Abs(transform.localScale.x) < Mathf.Abs(scale))
        {
            range = new Vector3(range.x * pow, range.y * pow, 0);
            speed = Mathf.Clamp(speed - (1f * times), 2, 20);
            if (jumpForce >= 0 && jumpForce <= 7)
                jumpForce = Mathf.Clamp(jumpForce - (1f * times), 0, 17);
            else
                jumpForce = Mathf.Clamp(jumpForce - (2f * times), 0, 17);
        }
        else
        {
            range = new Vector3(range.x / pow, range.y / pow, 0);
            speed = Mathf.Clamp(speed + (1f * times), 2, 20); 
            if (jumpForce >= 0 && jumpForce < 7)
                jumpForce = Mathf.Clamp(jumpForce + (1f * times), 0, 17);
            else
                jumpForce = Mathf.Clamp(jumpForce + (2f * times), 0, 17);
        }

        transform.localScale = new Vector3(scale, Mathf.Abs(scale), 0);

        height = Mathf.Abs(scale) * 100;

        if (transform.position.x < -37f || transform.position.y > 16.6) //avoids go out of screen
        {
            ChangeHealth(-100);
        }
    }

    public void ResetHeight()
    {
        range = new Vector3(-3, 0.3f, 0);
        jumpForce = 7;
        speed = 10;

        transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 0);

        height = DEFAULT_HEIGHT;

        if (currentLevel == (int) PHASES.BARRIER)
            ChangeHeight(false, 5);
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

    public Vector3 GetVelocity()
    {
        if (gameObject != null)
            if (!Mathf.Approximately(rigidbody2D.velocity.x,0) && !Mathf.Approximately(rigidbody2D.velocity.y,0))
                return rigidbody2D.velocity;
        
        return new Vector3(0,0,0);
    }
}
