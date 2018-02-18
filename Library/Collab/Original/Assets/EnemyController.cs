using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float speed = 2.0f;
	public float screenspeed = 1.0f;

	private int health = 20;
	private bool top;
	private bool bottom;
	Vector3 toppos;
	Vector3 bottompos;
	Vector3 leftpos;
	Vector3 rightpos;
	// Use this for initialization
	void Start () {
		top = false;
		bottom = true;
		toppos = Camera.main.ScreenToWorldPoint(new Vector3(0f, (float)Screen.height, 0f));
		bottompos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
		leftpos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
		rightpos = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, 0f, 0f));
	}

	// Update is called once per frame
	void Update () {
		leftpos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
		rightpos = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, 0f, 0f));
		if (transform.position.x < rightpos.x) {
			transform.position += Vector3.right * screenspeed * Time.deltaTime;
			if (transform.position.y > toppos.y) {
				top = true;
				bottom = false;
			}
			if (transform.position.y < bottompos.y) {
				bottom = true;
				top = false;
			}
			if (!top) {
				transform.position += Vector3.up * speed * Time.deltaTime;
			}
			if (!bottom) {
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		}
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Bullet") {
			Destroy (col.gameObject);
			health -= 1;
		}
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
