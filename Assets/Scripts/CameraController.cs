using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float speed = 1.0f;

	private float timeUntilStartFight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!LoadManager.instance.getIsPaused ()) {
			if (!LoadManager.instance.getGameOver ()) {
				if (transform.position.x < 80) {
					transform.position += Vector3.right * speed * Time.deltaTime;
				} else if (transform.position.x >= 80 && transform.position.x < 88) {
					LoadManager.instance.setIsBoss (true);
					timeUntilStartFight = Time.time + 4f;
					transform.position += Vector3.right * speed * Time.deltaTime;
				} else if (transform.position.x >= 88 && Time.time < timeUntilStartFight) {
					LoadManager.instance.setIsCutscene (true);
				} else if (Time.time > timeUntilStartFight) {
					LoadManager.instance.setIsCutscene (false);
					LoadManager.instance.setIsBoss (false);
				} 

			}
		}
	}
}