using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityObjects;

namespace Assets.Scripts.Character
{

    
    class CanDealDamage : MonoBehaviour
    {
        public Transform attackPosition = null;
        public LayerMask whatIsEnemies;
        public float attackRange = 0;
        public float defaultDamage = 0;

        public float timeBetweenAttacks = 0;

        private float attackTimer;
        private UnityObjects.Character character;

        private void Start()
        {
            character = transform.GetComponent<UnityObjects.Character>();
            Debug.Log(character.tag);
            attackTimer = 0;
        }

        private void Update()
        {

            attackTimer -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                if(attackTimer <= 0)
                {
                    DealDamage();
                    attackTimer = timeBetweenAttacks;
                }
            }
        }

        private void DealDamage()
        {

            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].GetComponent<UnityObjects.Character>().GetDamage(character.info.baseAttackDamage);
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPosition.position, attackRange);
        }
    }
}
