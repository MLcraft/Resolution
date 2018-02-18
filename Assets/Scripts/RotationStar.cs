
using System.Collections.Generic;
using UnityEngine;

public class RotationStar : MonoBehaviour {
	public float rotationspeed = 10f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!LoadManager.instance.getIsPaused ()) {
			transform.Rotate (0, 0, 80 * rotationspeed * Time.deltaTime);
		}
	}
}
