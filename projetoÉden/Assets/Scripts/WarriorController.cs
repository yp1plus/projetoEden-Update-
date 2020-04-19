using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpForce = 7;
    //private float moveInput;
    private bool isGrounded;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    // Use this for initialization
    void Start() 
    {  
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        //rigidbody2D.velocity = new Vector2(moveInput * speed, rigidbody2D.velocity.y);

    }

    // Update is called once per frame
    void Update()
    {
        //1 se direita, -1 se esquerda, 0 se nada
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log(horizontal);

        Vector2 move = new Vector2(horizontal, vertical); //minha posição, naõ posso normalizar

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize(); //normaliza o vetor porque só a direção é importante
        }

        animator.SetFloat("Move X", lookDirection.x);
        //animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        /*Seta a posição atual para mais 0.1*/
        //Vector2 position = transform.position;
        Vector2 position = rigidbody2D.position;
        //position.x = position.x + speed * horizontal * Time.deltaTime;
        //position.y = position.y + speed * vertical * Time.deltaTime;
        position = position + move * speed * Time.deltaTime;
        //transform.position = position;
        rigidbody2D.MovePosition(position);
        /*moveInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            rigidbody2D.velocity = Vector2.up * jumpForce;
        }

        Vector2 move = new Vector2(moveInput, 0);

        if (!Mathf.Approximately(moveInput, 0.0f))
        {
            lookDirection.Set(moveInput, 0);
            lookDirection.Normalize(); //normaliza o vetor porque só a direção é importante
        }
        
        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Speed", move.magnitude);

        if (rigidbody2D.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        } 
        else
        {
            animator.SetBool("isJumping", false);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Attack");
        }*/
    }

    /*protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            Debug.Log("KKK");
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f; //decrease half
            }
        }

        animator.SetBool ("Grounded", grounded);
        animator.SetFloat("Move X", move.x);
        animator.SetFloat ("Velocity X", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }*/
}
