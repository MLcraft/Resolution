
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 10.0f;
	public float padding = 1f;
	public GameObject StarProjectile; 
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public int health = 6;

	Vector3 leftmost;
	Vector3 rightmost;
	Vector3 upmost;
	Vector3 downmost;

	float xmin;
	float xmax;
	float ymin;
	float ymax;


	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3 (1,0,distance));
		Vector3 upmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,1,distance));
		Vector3 downmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
		ymin = downmost.y + padding;
		ymax = upmost.y - padding; 

		
	}

	void Fire(){
		Vector3 offset = new Vector3(1.2f, -0.25f, 0);
		GameObject StarShot = Instantiate (StarProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot.GetComponent<Rigidbody2D>().velocity = new Vector3 (projectileSpeed, 0, 0);
	}
	// Update is called once per frame
	void Update () {
		leftmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,0));
		rightmost = Camera.main.ViewportToWorldPoint(new Vector3 (1,0,0));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}

		 if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += new Vector3(0,speed * Time.deltaTime, 0);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position += new Vector3 (0, -speed * Time.deltaTime, 0);
		}

		if  (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += new Vector3 (-speed * Time.deltaTime, 0,0);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0,0);
		} 

		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp (transform.position.y, ymin, ymax);

		transform.position = new Vector3 (newX, newY, transform.position.z);

	}

	void OnCollisionStay2D(Collision2D col) {
		print ("in collision");
		if (col.gameObject.CompareTag ("Enemy")) {
			health -= 1;
			Destroy (col.gameObject);
		}
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
