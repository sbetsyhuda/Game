using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{

    public float defaultSpeed;
    public float defaultUpForce;
    public bool  facingRight;
    public float groundCheckRadius;

    public LayerMask whatIsGround;
    public Transform groundCheck;

    //public LayerMask whatIsWall;
    //public Transform WallCheck;

    private float moveInput;
    private int jumpCount;
    private bool isGrounded;
    //private bool hitWall;

    //private Animator animator;
    private Rigidbody2D rb2d;

    private void Start()
    {
        facingRight = true;
        //animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        jumpCount = 0;
        rb2d.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //hitWall = Physics2D.OverlapCircle(WallCheck.position, groundCheckRadius, whatIsWall); 
        if (isGrounded)
        {
            jumpCount = 0;
        }

        moveInput = Input.GetAxis("Horizontal");
        //if(hitWall)
        //{
        //    moveInput = 0;
        //}
        rb2d.velocity = new Vector2(defaultSpeed * moveInput, rb2d.velocity.y);

        if(facingRight && moveInput < 0 || !facingRight && moveInput > 0)
        {
            Flip();
        }

    }

    private void Update()
    {
        if (jumpCount == 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            rb2d.velocity = Vector2.up * defaultUpForce;
            jumpCount++;
        }
        
        //if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        //Gizmos.DrawWireSphere(WallCheck.position, groundCheckRadius);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
