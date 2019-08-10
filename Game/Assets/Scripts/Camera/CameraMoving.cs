﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
	public Transform character;
	public Vector3 offset;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		gameObject.transform.position = offset + character.position;
    }
}
