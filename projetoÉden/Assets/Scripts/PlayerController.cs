using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Atributes */
    public float speed = 3.0f;
    public float jumpForce = 7;
    // [Range(0, 1)]
    private float jumpHeight = .4f;
    public int maxHealth = 100;
    public int health { get {return currentHealth;} }
    private int currentHealth;

    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;

    /* For Jump */
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector3 range;

     /* Movement */
    private float moveInput;
    public float move {get { return moveInput;} }
    private bool facingRight = true;
    public bool facing {get {return facingRight;} }

     /* Control Invincibility Time */
    bool isInvincible = false;
    float invincibleTimer = 5.0f;

    protected SpriteRenderer mainRenderer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void Movement(Rigidbody2D rigidbody2D)
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        rigidbody2D.velocity = new Vector2(moveInput * speed, rigidbody2D.velocity.y);

        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (rigidbody2D.velocity.y > 0)
            {
                rigidbody2D.velocity  = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * jumpHeight);
            }
        }
    }

    public void BetterJumping(Rigidbody2D rigidbody2D)
    {
        if (rigidbody2D.velocity.y < 0)
        {
            rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rigidbody2D.velocity.y > 0 && Input.GetKeyUp(KeyCode.UpArrow))
        {
           rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    /// <summary>
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void CheckCollisionForJump(Rigidbody2D rigidbody2D, Animator animator)
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0, groundLayer);

        if (bottomHit != null)
        {
            if (bottomHit.gameObject.tag == "Ground" && Input.GetKeyDown(KeyCode.UpArrow))
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                animator.SetBool("isJumping", true);
            }
            else if (!AnimatorIsPlaying("Jump", animator))
            {
                animator.SetBool("isJumping", false);
            }
        }
    }

    private bool AnimatorIsPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public bool AnimatorIsPlaying(string name, Animator animator)
    {
        return AnimatorIsPlaying(animator) && animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }

    public virtual void ChangeHealth(int amount)
    {
        if (amount < 0) //want to take life
        {
            if (isInvincible)
                return;
            
            StartCoroutine(AnimateDamage());
            isInvincible = true;    
            Invoke("ResetInvincibility", invincibleTimer);
        }
        
        //The first value it is between the second and third
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); 
    }

    /* Flash the Player */
    IEnumerator AnimateDamage()
    {
        for (int i = 0; i < invincibleTimer; i++)
        {
            Debug.Log("Animate Damage");
            mainRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            mainRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        mainRenderer.enabled = true;
    }

    void ResetInvincibility()
    {
        isInvincible = false;
    }
}
