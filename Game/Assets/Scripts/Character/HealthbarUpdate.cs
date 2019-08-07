using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;
using UnityObjects;

public class HealthbarUpdate : MonoBehaviour
{

    private Character character;


    // Start is called before the first frame update
    void Start()
    {
        character = transform.parent.gameObject.transform.parent.gameObject.GetComponent<Character>();
        Debug.Log(transform.parent.gameObject.transform.parent.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(Mathf.Max(0,character.info.healthPoints / character.info.maxHealthPoints) , transform.localScale.y, transform.localScale.z);
    }

}
