using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float jumpSideForce;

    [SerializeField]
    private Transform jumpCastPosition;

    [SerializeField]
    private int jumpsCount;

    [SerializeField]
    private ParticleSystem bloodTrail;

    public int JumpsCount
    {
        get
        {
            return jumpsCount;
        }

        set
        {
            jumpsCount = value;
        }
    }

    private Rigidbody2D body;

    private Animator animator;

    private float lastGroundedTime = float.MaxValue;

    private List<Collider2D> collisions = new List<Collider2D>();

    private List<Vector2> normals = new List<Vector2>();

    private float lastJumpTime = 0.0f;

    private bool isGrounded = false;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //StartCoroutine(RestoreJumps());
        var ground = GetComponent<DestroyPlatform>();

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
            if (contact.normal.y < 0.1f)
                normals.Add(contact.normal);
    }

    private void FixedUpdate()
    {
        normals.Clear();
    }

    //private IEnumerator RestoreJumps()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(5.0f);

    //        jumpsCount++;
    //        if (jumpsCount > 10)
    //            jumpsCount = 10;
    //    }
    //}



    private void Update()
    {
        foreach (var normal in normals)
            Debug.DrawLine(transform.position, transform.position + (Vector3)normal, Color.red);

        var moveInput = Input.GetAxis("Horizontal");

        var targetXSpeed = moveInput * moveSpeed;
        var targetYSpeed = body.velocity.y;

        var canJumpOffTheWall = false;
        var additionalDirection = 0.0f;
        foreach (var collision in normals)
        {
            if (!isGrounded && lastJumpTime < Time.time + 0.25f)
            {
                additionalDirection = Mathf.Sign(collision.x);
                targetXSpeed = -Mathf.Sign(collision.x) * 1.0f;
                canJumpOffTheWall = true;
                if (targetYSpeed < -4.0f)
                    targetYSpeed = -4.0f;
            }
        }

        body.velocity = new Vector2(targetXSpeed, targetYSpeed);
        if (Mathf.Abs(moveInput) > 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

        if (Mathf.Abs(body.velocity.x) > 0.1f && isGrounded)
        {
            if (!bloodTrail.isPlaying)
                bloodTrail.Play();
        }
        else
            bloodTrail.Stop();

        Physics2D.OverlapCircle(jumpCastPosition.position, 0.1f, new ContactFilter2D(), collisions);
        if (collisions.Find(x => x.GetComponent<Ground>()) != null)
        {
            animator.SetBool("IsGrounded", true);
            lastGroundedTime = Time.time;
            isGrounded = true;
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && jumpsCount != 0 
            && lastGroundedTime + 0.15f > Time.time
            || Input.GetButtonDown("Jump") && canJumpOffTheWall && jumpsCount != 0)
            Jump(additionalDirection);
            
        animator.SetBool("Run", Mathf.Abs(body.velocity.x) > 0.1f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Restarter>() != null || collision.GetComponent<Saw>() != null)
            Destroy(gameObject);
           
    }

    private void Jump(float additionalDirection)
    {
        body.velocity = Vector2.up * jumpForce;
        body.velocity += Vector2.right * additionalDirection * jumpSideForce;
        jumpsCount--;
        lastJumpTime = Time.time;

        if(jumpsCount <= 0 )
            Destroy(gameObject);
    }
}
