using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAutoDestroy : MonoBehaviour {



	Vector3 upmost;
	Vector3 leftmost;

	float xmax;
	float xmin;

	// Use this for initialization
	void Start () {
		upmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0));
		leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));

		xmax = upmost.x;
		xmin = leftmost.x;
		
	}

	// Update is called once per frame
	void Update () {
			if (!LoadManager.instance.getGameOver ()) {
			upmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0));
			leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
			xmax = upmost.x;
			xmin = leftmost.x;
				if (gameObject.transform.position.x < (xmin - 0.5f)) {
					Destroy (gameObject);
				}
				if (gameObject.transform.position.x > (xmax + 0.5f)) {
					Destroy (gameObject);
				}
			} else {
				Destroy (gameObject);
			}

	}
}
