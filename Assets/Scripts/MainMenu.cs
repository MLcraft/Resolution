using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    private MainMenuController _mainMenuController;

	// Initialization
	void Start () {
        _mainMenuController = new MainMenuController();

        _mainMenuController.ShowMainMenu(true);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
