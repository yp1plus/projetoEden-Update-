using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement and the health of a generic player.
/// </summary>
///<remarks>
/// <para> This class can control movement to the left and to the right, and also jump, with arrows of keyboard. </para>
/// <para> This class also can control health of player, both adding and decreasing, invoking a flash animation when life is lost. </para>
///</remarks>
public class PlayerController : MonoBehaviour
{
    /* Atributes */
    public float speed = 3.0f;
    public float jumpForce = 7;
    public int maxHealth = 100;

    /// <value> Gets the value of current health of player. </value>
    public int health { get {return currentHealth;} }
    protected int currentHealth;

    float fallMultiplier = 6f;
    float lowJumpMultiplier = 2f;

    /* For Jump */
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector3 range;

    /* Movement */
    /// <value> Gets the value of horizontal move input. </value>
    public float move {get { return moveInput;} }
    float moveInput;
   
    /// <value> Gets the value of a bool which controls if player are facing right. </value>
    public bool facing {get {return facingRight;} }
    protected bool facingRight = true;

     /* Control Invincibility Time */
    /// <value> Gets the value of a bool which controls if player are invincible. </value>
    public bool invincible {get {return isInvincible;}}
    protected bool isInvincible;

    protected float invincibleTimer = 5.0f;

    protected Fade fade;
    public static bool isGround {get; private set;} = false;

    bool isDead = false;

    public virtual void Start()
    {
        fade = gameObject.AddComponent<Fade>();
        currentHealth = maxHealth;
        isInvincible = false;
    }

    /// <summary>
    /// Controls the movement of player from horizontal and up input arrows.
    /// </summary>
    /// <remarks> This method can be override in subclasses. </remarks>
    /// <param name = "rigidbody2D"> The component Rigidbody2D of player. </param>
    public virtual void Movement(Rigidbody2D rigidbody2D)
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        rigidbody2D.velocity = new Vector2(moveInput * speed, rigidbody2D.velocity.y);

        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Controls hardier the function of jump, allowing a jump more subtle.
    /// </summary>
    /// <remarks> This method is not finished yet. </remarks>
    /// <param name = "rigidbody2D"> The component Rigidbody2D of player. </param>
    public void BetterJumping(Rigidbody2D rigidbody2D)
    {
        if (rigidbody2D.velocity.y < 0)
        {
            rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rigidbody2D.velocity.y > 0 && Input.GetAxisRaw("Vertical") == 0)
        {
           rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    /// <summary>
    /// Checks if the player is on the ground when up arrow is pressed.
    /// </summary>
    /// <param name = "rigidbody2D"> The component Rigidbody2D of player. </param>
    /// <param name = "animator"> The component Animator of a player to ativate animation of jump. </param>
    public void CheckCollisionForJump(Rigidbody2D rigidbody2D, Animator animator)
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0, groundLayer);

        BetterJumping(rigidbody2D);

        if (bottomHit != null)
        {
            if (bottomHit.CompareTag("Ground"))
                isGround = true;
            
            if ((bottomHit.CompareTag("Ground") || bottomHit.CompareTag("Block")) && Input.GetAxisRaw("Vertical") == 1)
            {
                rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);    
            }
            else if (!AnimatorIsPlaying("Jump", animator))
            {
                animator.SetBool("isJumping", false);   
            }
        }
    }

    /// <summary>
    /// Checks if any animation is playing.
    /// </summary>
    /// <param name = "animator"> The component Animator of a player. </param>
    /// <returns> The answer if any animation is playing. </returns>
    public static bool AnimatorIsPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    /// <summary>
    /// Checks if a specific animation is playing.
    /// </summary>
    /// <param name = "name"> A string, the name of animation. </param>
    /// <param name = "animator"> The component Animator of a player. </param>
    /// <returns> The answer if a specific animation is playing. </returns>
    public bool AnimatorIsPlaying(string name, Animator animator)
    {
        return AnimatorIsPlaying(animator) && animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    /// <summary>
    /// Executes the flip of player when called.
    /// </summary>
    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
        
        if (transform.childCount != 0) //warrior's name
        {
            Debug.Log("Child Flipped");
            Vector3 childScale = transform.GetChild(0).transform.localScale;
            if (childScale.x < 0)
                childScale.x  *= -1;
            transform.GetChild(0).transform.localScale = childScale;
        }
    }

    /// <summary>
    /// Changes the health of player, respecting the maximum health.
    /// </summary>
    /// <remarks> 
    /// <para> Positive amounts increases health and negative amounts decreases it. </para>
    /// <para> If amount it is less than zero, the player flash. If health of player expires, the player is destroyed. </para>
    /// <para> This method can be override in subclasses. </para>
    /// </remarks>
    /// <param name = "amount"> A integer number, the amount of health to add or remove. </param>
    public virtual void ChangeHealth(int amount)
    {
        if (health == 0)
            return;

        if (amount < 0) //want to take life
        {
            if (isInvincible)
                return;

            isInvincible = true;

            if (currentHealth + amount > 0)
                FlashPlayer();
        }
        
        //The first value it is between the second and third
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    void FlashPlayer()
    {
        StartCoroutine(AnimateDamage());
    }

    public void DestroyPlayerDead()
    {
        if (health == 0 && gameObject != null && gameObject.activeSelf && !isDead)
        {
            isDead = true;
            StartCoroutine(EnumeratorPlayerDead());
        }
    }

    public void DestroyPlayerDead(Animator animator)
    {
        if (health == 0)
        {
            if (animator != null)
                animator.SetTrigger("isDead");
            else
                fade.FadeOut();
        }   
    }

    IEnumerator EnumeratorPlayerDead()
    {
        fade.FadeOut();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    /* Flash the Player for a while */
    IEnumerator AnimateDamage()
    {   
        for (int i = 0; i < invincibleTimer; i++) 
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<Renderer>().enabled = true;
        
        ResetInvincibility();
    }

    protected void ResetInvincibility()
    {
        isInvincible = false;
    }
}
