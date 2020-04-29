using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;

    private bool FacingRight = true;

    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    private int extraJumps;
    public int extraJumpsValue;

    private Animator animator;

    private void Start()
    {
        extraJumps = extraJumpsValue;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    { 
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(moveInput * speed, body.velocity.y);

        if (FacingRight == false && moveInput > 0)
            Flip();
        else if (FacingRight == true && moveInput < 0)
            Flip();
            
    }
    private void Update()
    {
        var currentSpeed = body.velocity.x;

        if (isGrounded == true)
            extraJumps = extraJumpsValue;

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            Jump();
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
            Jump();

        animator.SetBool("IsWalking", Mathf.Abs(currentSpeed) > 0.05f && isGrounded);
    }
    void Jump()
    {
        body.velocity = Vector2.up * jumpForce;
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
