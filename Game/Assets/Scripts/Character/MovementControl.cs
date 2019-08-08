using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{

    public float defaultSpeed;
    public float defaultUpForce;
    public int facingRight;

    private int jumpCount;
    private Animator animator;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = 1;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        jumpCount = 0;
        rb2d.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalSpeed = 0;
        if (jumpCount == 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            rb2d.AddForce(new Vector2(0, defaultUpForce));
            jumpCount++;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalSpeed -= defaultSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {

        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalSpeed += defaultSpeed;
        }
        if (horizontalSpeed < 0 && facingRight == 1)
        {
            facingRight = 0;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (horizontalSpeed > 0 && facingRight == 0)
        {
            facingRight = 1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            jumpCount = 0;
        }
    }
}
