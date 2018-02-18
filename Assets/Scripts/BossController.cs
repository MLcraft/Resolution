using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	public GameObject EnemyProjectile;
	public float projectileSpeed = 8f;
	public float fireRate = 1f;
	public AudioClip firesound;
	public int health = 300;

	private GameObject Player;

	private float nextFire = 1.0f;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		
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
		GameObject StarShot4 = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot4.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed * (0.5f), projectileSpeed*(Mathf.Sqrt(3)/2), 0);
		StarShot4.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed * (0.5f);
		StarShot4.GetComponent<ProjectileController> ().projectileSpeedy = projectileSpeed*(Mathf.Sqrt(3)/2);
		GameObject StarShot5 = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot5.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed*(0.5f), -projectileSpeed*(Mathf.Sqrt(3)/2), 0);
		StarShot5.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed * (0.5f);
		StarShot5.GetComponent<ProjectileController> ().projectileSpeedy = -projectileSpeed*(Mathf.Sqrt(3)/2);
		GameObject StarShot6 = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot6.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed*(Mathf.Sqrt(3)/2), projectileSpeed*(0.5f), 0);
		StarShot6.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed*(Mathf.Sqrt(3)/2);
		StarShot6.GetComponent<ProjectileController> ().projectileSpeedy = projectileSpeed * (0.5f);
		GameObject StarShot7 = Instantiate (EnemyProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		StarShot7.GetComponent<Rigidbody2D>().velocity = new Vector3 (-projectileSpeed*(Mathf.Sqrt(3)/2), -projectileSpeed*(0.5f), 0);
		StarShot7.GetComponent<ProjectileController> ().projectileSpeedx = -projectileSpeed*(Mathf.Sqrt(3)/2);
		StarShot7.GetComponent<ProjectileController> ().projectileSpeedy = -projectileSpeed * (0.5f);
		AudioSource.PlayClipAtPoint (firesound, transform.position);
		AudioSource.PlayClipAtPoint (firesound, transform.position);
		AudioSource.PlayClipAtPoint (firesound, transform.position);
		AudioSource.PlayClipAtPoint (firesound, transform.position);
		AudioSource.PlayClipAtPoint (firesound, transform.position);
		AudioSource.PlayClipAtPoint (firesound, transform.position);
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
		if (LoadManager.instance.getGameOver ()) {
			Destroy (gameObject);
		} else if (LoadManager.instance.getIsBoss () && LoadManager.instance.getIsCutscene ()) {
			// put some animation stuff here
		} else {
			if ((Camera.main.transform.position.x >= 88) && (Time.time > nextFire)) {
				nextFire = Time.time + fireRate;
				int shot = (int)Mathf.Floor (Random.Range(0, 5));
				if (shot == 0) {
					FireSpreadShot ();
				} else if (shot == 1) {
					FireAim ();
				} else if (shot == 2) {
					FireAim ();
				} else if (shot == 3) {
					FireAim ();
				} else if (shot == 4) {
					FireSpreadShot ();
				} else if (shot == 5) {
					FireAim ();
				}
			}
		}
	}

	void OnCollisionStay2D (Collision2D col) {
		if (!LoadManager.instance.getIsPaused ()) {
			if (col.gameObject.CompareTag ("Bullet")) {
				if (!col.gameObject.GetComponent<ProjectileController> ().GetIsPixel ()) {
					Destroy (col.gameObject);
					health -= 1;
				}
			}

			if (health <= 0) {
				Destroy (gameObject);
				GameHudController.instance.ShowGameEnd (true);
			}
		}
	}
}
