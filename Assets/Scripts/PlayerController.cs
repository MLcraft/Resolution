
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float speed = 10.0f;
	public float padding = 1f;
	public GameObject StarProjectile;
	public GameObject PixelProjectile;
	public float projectileSpeed = 30f;
	public float firingRate = 0.2f;
	public int health = 6;
	public float invincibleDuration = 2.0f;


	public AudioClip firesound;
	public AudioClip gameover;

	Vector3 leftmost;
	Vector3 rightmost;
	Vector3 upmost;
	Vector3 downmost;

	float xmin;
	float xmax;
	float ymin;
	float ymax;

	private float nextFire = 0.0f;
	private float invincibleStart = 0.0f;
	private bool invincible = false;
    private bool isPixel = false;

    public GameObject[] floatingStars;
    public Sprite normalStar;
    public Sprite pixelStar;

	// Use this for initialization
	void Start () {
		SceneManager.SetActiveScene (gameObject.scene);
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

		if (!isPixel) {
			GameObject StarShot = Instantiate (StarProjectile, transform.position + offset, Quaternion.identity) as GameObject;
			StarShot.GetComponent<Rigidbody2D> ().velocity = new Vector3 (projectileSpeed, 0, 0);
			StarShot.GetComponent<ProjectileController> ().projectileSpeedx = projectileSpeed;
		} else {
			GameObject StarShot = Instantiate (PixelProjectile, transform.position + offset, Quaternion.identity) as GameObject;
			StarShot.GetComponent<Rigidbody2D> ().velocity = new Vector3 (projectileSpeed, 0, 0);
			StarShot.GetComponent<ProjectileController> ().projectileSpeedx = projectileSpeed;
			StarShot.GetComponent<ProjectileController>().SetIsPixel(isPixel);
		}
        

        AudioSource.PlayClipAtPoint (firesound, transform.position);
	}

	// Update is called once per frame
	void Update () {
		leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
		if (!LoadManager.instance.getIsPaused ()) {
			if ((invincibleStart + invincibleDuration) < Time.time) {
				invincible = false;
			}
			if (!LoadManager.instance.getIsBoss()) {
				if (Input.GetKey (KeyCode.Space)) {
					if (Time.time > nextFire) {
						nextFire = Time.time + firingRate;
						Fire ();
					}
				}

				if (Input.GetKey (KeyCode.UpArrow)) {
					transform.position += new Vector3 (0, speed * Time.deltaTime, 0);
				} else if (Input.GetKey (KeyCode.DownArrow)) {
					transform.position += new Vector3 (0, -speed * Time.deltaTime, 0);
				}

				if (Input.GetKey (KeyCode.LeftArrow)) {
					transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
				} else if (Input.GetKey (KeyCode.RightArrow)) {
					transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
				} 

				float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
				float newY = Mathf.Clamp (transform.position.y, ymin, ymax);

				transform.position = new Vector3 (newX, newY, transform.position.z);
		
				if (Input.GetKeyUp (KeyCode.Space)) {
					CancelInvoke ("Fire");
				}

				// this is to switch between normal and pixel modes
				if (Input.GetKeyUp (KeyCode.S)) {
					isPixel = !isPixel;

					Sprite spriteStar;
					if (isPixel) {
						spriteStar = pixelStar;
					} else {
						spriteStar = normalStar;
					}

					for (int i = 0; i < floatingStars.Length; i++) {
						floatingStars [i].GetComponent<SpriteRenderer> ().sprite = spriteStar;
					}

				}
			} else {
				transform.position = new Vector3 (xmin, 0, 0);
			}
        }

        // for pause menu
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(!LoadManager.instance.getIsPaused())
            {
                LoadManager.instance.setIsPaused(true);
                GameHudController.instance.ShowPauseMenu(true);
            } else
            {
                LoadManager.instance.setIsPaused(false);
                GameHudController.instance.ShowPauseMenu(false);
            }
        }

    }



	void OnCollisionStay2D(Collision2D col) {
		if (!LoadManager.instance.getIsPaused ()) {
			if (col.gameObject.CompareTag ("EnemyBullet") || col.gameObject.CompareTag("Cloud")) {
				if (!invincible) {
					invincibleStart = Time.time;
					invincible = true;
					health -= 1;
					// change game hud health
					GameHudController.instance.ShowHealth (health);

				}

				Destroy (col.gameObject);
			} else if (col.gameObject.CompareTag ("Enemy")) {
				if (!invincible) {
					invincibleStart = Time.time;
					invincible = true;
					health -= 1;
					// change game hud health
					GameHudController.instance.ShowHealth (health);

				}
			} else if (col.gameObject.CompareTag ("Health Pickup")) {
				health += 2;
					
				if (health > 6) {
					health = 6;
				}
				// change game hud health
				GameHudController.instance.ShowHealth (health);

				Destroy (col.gameObject);
			}
			if (health <= 0) {
				GameHudController.instance.ShowHealth (health);
				Destroy (gameObject);

				// bring up the game over screen
				stopAllAudio();
				LoadManager.instance.setGameOver (true);
				AudioSource.PlayClipAtPoint (gameover, Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0,0)));
				GameHudController.instance.ShowGameOver (true);
			}
		}
	}

	void stopAllAudio() {
		AudioSource[] allAudio = FindObjectsOfType<AudioSource> ();
		foreach (AudioSource a in allAudio) {
			a.Stop();
		}
	}

	public float getProjectileSpeed() {
		return projectileSpeed;
	}
//	IEnumerator GameOver() {
//		yield return new WaitForSeconds (1);
//	}
}
