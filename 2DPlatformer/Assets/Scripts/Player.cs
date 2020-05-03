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
        if (body.velocity.y < 0.1f)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        else
            isGrounded = false;

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

        if (isGrounded)
            extraJumps = 0;

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            isGrounded = false;
            Jump();
            extraJumps--;
            if (extraJumps == 0)
                StartCoroutine(MakeFlip());
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            isGrounded = false;
            Jump();
            extraJumps = extraJumpsValue;
        }

        animator.SetBool("IsWalking", Mathf.Abs(currentSpeed) > 0.05f && isGrounded);
        animator.SetBool("IsGrounded", isGrounded);

        if (Input.GetMouseButtonDown(0))
            animator.Play(isGrounded ? "Punch" : "PunchMidAir");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
            return;

        if (other.GetComponent<EnnemyScript>())
            Destroy(other.gameObject);
    }

    private IEnumerator MakeFlip()
    {
        animator.Play("Flip");

        var rotation = 0.0f;
        while (rotation < 360.0f)
        {
            var rotationAmount = 720 * Time.deltaTime;

            transform.Rotate(Vector3.forward * rotationAmount);

            rotation += rotationAmount;

            yield return null;
        }

        var angle = transform.eulerAngles;
        angle.z = 0;
        transform.eulerAngles = angle;
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
