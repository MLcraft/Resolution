using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
	public float projectileSpeedx;
	public float projectileSpeedy;

    private bool isPixel = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!LoadManager.instance.getIsBoss ()) {
			if (!LoadManager.instance.getIsPaused ()) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (projectileSpeedx, projectileSpeedy, 0);
			} else if (LoadManager.instance.getIsPaused ()) {
				gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			}
		} else {
			Destroy (gameObject);
		}
	}

    public void SetIsPixel(bool isItPixel)
    {
        isPixel = isItPixel;
    }

    public bool GetIsPixel()
    {
        return isPixel;
    }
}
