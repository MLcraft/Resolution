﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAutoDestroy : MonoBehaviour {

	float xmax;


	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 upmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));

		xmax = upmost.x;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x > (xmax + 0.5f)) {
			Destroy (gameObject);
		}
		
	}
}
