using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShotEnemy : MonoBehaviour {
	public float speed = 2.0f;
	public float screenspeed = 1.0f;
	public GameObject EnemyProjectile;
	public float projectileSpeed = 8f;
	public float fireRate = 0.5f;
	public float flyoffscreen = 30f;

	public AudioClip firesound;

	private GameObject Player;
	private float nextFire = 0.0f;
	private int health = 18;
	private bool top;
	private bool bottom;
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
		AudioSource.PlayClipAtPoint (firesound, transform.position);
		AudioSource.PlayClipAtPoint (firesound, transform.position);

	}

	void FireStraight() {
		Vector3 offset = new Vector3(-1.2f, -0.25f, 0);
		GameObject StarShot = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed, 0, 0);
		StarShot.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed;
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

		if (!LoadManager.instance.getIsPaused ()) {
			if (LoadManager.instance.getGameOver ()) {
				Destroy (gameObject);
			} else {
				leftpos = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
				rightpos = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0));
				if (!LoadManager.instance.getIsBoss()) {
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
						if (Time.time > nextFire) {
							nextFire = Time.time + fireRate;
							FireSpreadShot ();
						}
					}
				} else {
					print (transform.position.y);
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

	void OnCollisionEnter2D(Collision2D col){
		if (!LoadManager.instance.getIsPaused ()) {
			if (col.gameObject.CompareTag ("Bullet")) {
				if(!col.gameObject.GetComponent<ProjectileController>().GetIsPixel())
				{
					Destroy (col.gameObject);
					health -= 1;
				} else
				{
					Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
				}
			}

			if (health <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
