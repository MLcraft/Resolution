﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHudController {

    private GameHudViewPresenter _gameHudView;
    private PauseMenuViewPresenter _pauseMenuView;
    private GameOverViewPresenter _gameOverView;
    private LoadManager loadManager;

    private static GameHudController _instance = null;
    public static GameHudController instance
    {
        get
        {
            return _instance;
        }
    }

    public GameHudController()
    {
        _instance = this;

        _gameHudView = GameObject.Find("UI/Canvas/Panel_GameHud").GetComponent<GameHudViewPresenter>();
        _gameHudView.Show(false);

        _pauseMenuView = GameObject.Find("UI/Canvas/Panel_PauseMenu").GetComponent<PauseMenuViewPresenter>();
        _pauseMenuView.Show(false);

        _gameOverView = GameObject.Find("UI/Canvas/Panel_GameOver").GetComponent<GameOverViewPresenter>();
        _gameOverView.Show(false);

        _gameHudView.PauseButton.Clicked += Pause;
        _pauseMenuView.ResumeButton.Clicked += Resume;
        _pauseMenuView.RestartButton.Clicked += Restart;
        _pauseMenuView.MainMenuButton.Clicked += GoToMainMenu;
        _gameOverView.AgainButton.Clicked += Restart;
        _gameOverView.MainMenuButton.Clicked += GoToMainMenu;

        loadManager = (LoadManager)GameObject.Find("LoadManager").GetComponent(typeof(LoadManager));

    }

    public void ShowGameHud(bool show)
    {
        _gameHudView.Show(show);
    }

    public void ShowPauseMenu(bool show)
    {
        _pauseMenuView.Show(show);
    }

    public void ShowGameOver(bool show)
    {
        _gameOverView.Show(show);
    }

    private void Pause(object sender, EventArgs args)
    {
		LoadManager.instance.setIsPaused (true);
        ShowPauseMenu(true);
    }

    private void Resume(object sender, EventArgs args)
    {
        ShowPauseMenu(false);
		LoadManager.instance.setIsPaused (false);
    }

    private void Restart(object sender, EventArgs args)
    {
        // TO DO, probably have to make it so it doesn't reset the ui too
		LoadManager.instance.setIsPaused (false);
		LoadManager.instance.setGameOver (true);
        loadManager.LoadGame();
    }

    private void GoToMainMenu(object sender, EventArgs args)
    {
		LoadManager.instance.setIsPaused (false);
		LoadManager.instance.setGameOver (true);
		loadManager.LoadMainMenu();
    }

    public void ShowHealth(int health)
    {
        _gameHudView.ShowHealth(health);
    }

}
