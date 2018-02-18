using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float speed = 2.0f;
	public float screenspeed = 1.0f;
	public GameObject EnemyProjectile;
	public float projectileSpeed = 8f;
	public float fireRate = 0.5f;
	public float flyoffscreen = 3f;
	public float changeRate = 1f;

	public AudioClip firesound;

	private bool inView = false;
	private GameObject Player;
	private float nextFire = 1.0f;
	private float nextChange = 0.0f;
	private int health = 12;
	private bool top;
	private bool bottom;
	float speedx;
	float speedy;
	Vector3 toppos;
	Vector3 bottompos;
	Vector3 leftpos;
	Vector3 rightpos;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		top = false;
		bottom = true;
		leftpos = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,0));
		rightpos = Camera.main.ViewportToWorldPoint(new Vector3 (1,0,0));
		toppos = Camera.main.ViewportToWorldPoint(new Vector3 (0,1,0));
		bottompos = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,0));
	}

	void FireSpreadShot() {
		Vector3 offset = new Vector3(-1.2f, -0.25f, 0);
		GameObject StarShot = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed;
		StarShot.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed, 0, 0);
		GameObject StarShot2 = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot2.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed/(Mathf.Sqrt(2)), projectileSpeed/(Mathf.Sqrt(2)), 0);
		StarShot2.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed/(Mathf.Sqrt(2));
		StarShot2.GetComponent<ProjectileController> ().projectileSpeedy = projectileSpeed/(Mathf.Sqrt(2));
		GameObject StarShot3 = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot3.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed/(Mathf.Sqrt(2)), -projectileSpeed/(Mathf.Sqrt(2)), 0);
		StarShot3.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed/(Mathf.Sqrt(2));
		StarShot3.GetComponent<ProjectileController> ().projectileSpeedy = -projectileSpeed/(Mathf.Sqrt(2));

		AudioSource.PlayClipAtPoint (firesound, transform.position);
	}

	void FireStraight() {
		Vector3 offset = new Vector3(-1.2f, -0.25f, 0);
		GameObject StarShot = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed, 0, 0);
		StarShot.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed;

		AudioSource.PlayClipAtPoint (firesound, transform.position);
	}

	void FireAim() {
		Vector3 offset = new Vector3(-1.2f, -0.25f, 0);
		float xdistance = (Player.transform.position.x - gameObject.transform.position.x);
		float ydistance = (Player.transform.position.y - gameObject.transform.position.y);
		float distance = Mathf.Sqrt ((xdistance * xdistance) + (ydistance * ydistance));
		GameObject StarShot = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot.GetComponent<Rigidbody2D>().velocity = new Vector3 (projectileSpeed * (xdistance/distance), projectileSpeed * (ydistance/distance), 0);
		StarShot.GetComponent<ProjectileController> ().projectileSpeedx = projectileSpeed * (xdistance/distance);
		StarShot.GetComponent<ProjectileController> ().projectileSpeedy = projectileSpeed * (ydistance / distance);

		AudioSource.PlayClipAtPoint (firesound, transform.position);
	}

	// Update is called once per frame
	void Update () {
		Vector2 velocity = GetComponent<Rigidbody2D> ().velocity;
		if (LoadManager.instance.getGameOver ()) {
			Destroy (gameObject);
		} else {
			if (!LoadManager.instance.getIsRestart ()) {
				if (transform.position.x < rightpos.x) {
					inView = true;
				}
				if (!LoadManager.instance.getIsPaused ()) {
					leftpos = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0, 0));
					rightpos = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0));
					if (!LoadManager.instance.getIsBoss ()) {
						if (inView) {
							if (Time.time > nextFire) {
								nextFire = Time.time + fireRate;
								int shot = (int)Mathf.Floor (Random.Range (0, 2));
								if (shot == 0) {
									print (transform.position.x);
									FireSpreadShot ();
								} else if (shot == 1) {
									FireStraight ();
									print (transform.position.x);
								} else if (shot == 2) {
									FireAim ();
									print (transform.position.x);
								}
							}
							if (Time.time > nextChange) {
								nextChange = Time.time + changeRate;
								speedx = Random.Range (-1.0f, 2.0f) * speed;
								speedy = Random.Range (-1.0f, 1.0f) * speed;
							}
							velocity.x = speedx;
							velocity.y = speedy;
							if (transform.position.y > (toppos.y - 1)) {
								velocity.y = -2 * speed;
							}
							if (transform.position.y < (bottompos.y + 1)) {
								velocity.y = 2 * speed;
							}
							if (transform.position.x > (rightpos.x - 1)) {
								velocity.x = -2 * speed;
							}
							if (transform.position.x < (leftpos.x + 1)) {
								velocity.x = 2 * speed;
							}
							transform.position += new Vector3 (velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);
						}
					} else {
						if (transform.position.y > 0) {
							transform.position += new Vector3 (flyoffscreen * Time.deltaTime, flyoffscreen * Time.deltaTime, 0);
						}
						if (transform.position.y < 0) {
							transform.position += new Vector3 (flyoffscreen * Time.deltaTime, -flyoffscreen * Time.deltaTime, 0);
						}
						if (transform.position.y > toppos.y) {
							Destroy (gameObject);
						}
						if (transform.position.y < bottompos.y) {
							Destroy (gameObject);
						}
					}
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (!LoadManager.instance.getIsPaused ()) {
			if (col.gameObject.CompareTag ("Bullet")) {
                if(!col.gameObject.GetComponent<ProjectileController>().GetIsPixel())
                {
						Destroy (col.gameObject);
						health -= 1;
                }
			}

			if (health <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
