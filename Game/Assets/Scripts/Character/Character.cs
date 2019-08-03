using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityObjects
{
    public class Character : MonoBehaviour
    {
        CharacterInfo characterInfo;
        public bool isControlable;
        public float defaultSpeed;
        public float defaultUpForce;

        public int jumpCount;
        private Animator animator;
        private Rigidbody2D rb2d;
        

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            jumpCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(isControlable)
            {
                float horizontalSpeed = 0;
                if (jumpCount == 0 && Input.GetKey(KeyCode.W))
                {
                    rb2d.AddForce(new Vector2(0, defaultUpForce));
                    jumpCount++;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    horizontalSpeed -= defaultSpeed;
                }
                if (Input.GetKey(KeyCode.S))
                {

                }
                if (Input.GetKey(KeyCode.D))
                {
                    horizontalSpeed += defaultSpeed;
                }
                rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);
                
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "floor")
            {
                jumpCount = 0;
            }
        }
    }
}


