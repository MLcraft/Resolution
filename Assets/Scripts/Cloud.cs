using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    private int health = 5;

	void Update() {
		if (LoadManager.instance.getIsBoss ()) {
			Destroy (gameObject);
		}
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!LoadManager.instance.getIsPaused())
        {
            if (col.gameObject.CompareTag("Bullet"))
            {
                if (col.gameObject.GetComponent<ProjectileController>().GetIsPixel())
                {
                    Destroy(col.gameObject);
                    health -= 1;
                }
                else
                {
                    Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
