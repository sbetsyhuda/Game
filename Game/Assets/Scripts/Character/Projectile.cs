using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;
using UnityObjects;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifetime;
    public float distance;
    public LayerMask enemies;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, distance, enemies);
        if(hitinfo.collider != null)
        {
            Debug.Log("damage");
            hitinfo.collider.gameObject.GetComponent<Character>().GetDamage(new Damage(10, 10));
            DestroyProjectile();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
