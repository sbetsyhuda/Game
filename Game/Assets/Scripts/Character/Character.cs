using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;

namespace UnityObjects
{
    public class Character : MonoBehaviour
    {
        GameCharacterInfo characterInfo;
        public bool isControlable;
        public float defaultSpeed;
        public float defaultUpForce;
        public float timeBetweenAttacks;

        public Transform attackPosition;
        public LayerMask whatIsEnemies;
        public float attackRange;
        public float defaultDamage;

        private float attackTimer;
        private int jumpCount;
        private Animator animator;
        private Rigidbody2D rb2d;
        

        // Start is called before the first frame update
        void Start()
        {
            characterInfo = new GameCharacterInfo();
            attackTimer = 0;
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            jumpCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(isControlable)
            {
                attackTimer -= Time.deltaTime;
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
                rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);
                if(Input.GetKey(KeyCode.Space))
                {
                    if(attackTimer <=0)
                    {
                        DealDamage();
                        attackTimer = timeBetweenAttacks;
                    }
                    
                }
            }
        }

        private void DealDamage()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemies);
            for(int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].GetComponent<Character>().GetDamage(defaultDamage);
            }
        }

        public void GetDamage(float damage)
        {
            characterInfo.healthPoints -= damage;
            Debug.Log(string.Format("{0} damage taken", damage));
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


