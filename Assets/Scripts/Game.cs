using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    private GameHudController _gameHudController;

	// Initialization
	void Start () {
        _gameHudController = new GameHudController();

        _gameHudController.ShowGameHud(true);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
