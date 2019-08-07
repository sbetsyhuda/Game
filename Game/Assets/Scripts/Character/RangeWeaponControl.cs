using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponControl : MonoBehaviour
{
    public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    private float attackTimer = 0;
    public float defaultTimeBetweenAttacks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);
        if(attackTimer <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                attackTimer = defaultTimeBetweenAttacks;
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
        
    }
}
