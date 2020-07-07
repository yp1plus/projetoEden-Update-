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
    bool facingRight = true;

     /* Control Invincibility Time */
    /// <value> Gets the value of a bool which controls if player are invincible. </value>
    public bool invincible {get {return isInvincible;}}
    bool isInvincible;

    float invincibleTimer = 5.0f;

    Fade fade;

    public virtual void Start()
    {
        fade = gameObject.AddComponent<Fade>();
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
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    void OnBecameInvisible()
    {
        if (gameObject.GetComponent<Renderer>().enabled) //avoids to destroy the player when him are flashing 
        {
            currentHealth = 0;
            Destroy(gameObject);
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
            if (bottomHit.gameObject.tag == "Ground" && Input.GetAxisRaw("Vertical") == 1)
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
    private bool AnimatorIsPlaying(Animator animator)
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
    }

    /// <summary>
    /// Changes the health of player, respecting the maximum health.
    /// </summary>
    /// <remarks> 
    /// <para> Positive amounts increases health and negative amounts decreases it. </para>
    /// <para> If amount it is less than zero, the player flash. If health of player expires, the player is destroyed. </para>
    /// <para> This method can be override in subclasses. </para>
    /// </remarks>
    /// <param name = "amount"> A integer number, the amount of health to add or remove. </para>
    public virtual void ChangeHealth(int amount)
    {
        if (amount < 0) //want to take life
        {
            if (isInvincible)
                return;

            isInvincible = true;

            FlashPlayer();
        }
        
        //The first value it is between the second and third
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        DestroyPlayerDead();
    }

    void FlashPlayer()
    {
        StartCoroutine(AnimateDamage());
    }

    void DestroyPlayerDead()
    {
        if (health == 0)
        {
            fade.StartFade(1); //fade out
        }

    }

    /* Flash the Player for a while */
    IEnumerator AnimateDamage()
    {
        for (int i = 0; i < invincibleTimer; i++) 
        {
            Debug.Log("Animate Damage");
            gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<Renderer>().enabled = true;
        
        ResetInvincibility();
    }

    void ResetInvincibility()
    {
        isInvincible = false;
    }
}
