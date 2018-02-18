using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController
{
    private MainMenuViewPresenter _mainMenuView;
    private ButtonKeysViewPresenter _buttonKeysView;
    private LoadManager loadManager;

    private static MainMenuController _instance = null;
    public static MainMenuController instance
    {
        get
        {
            return _instance;
        }
    }

    public MainMenuController()
    {
        _instance = this;

        _mainMenuView = GameObject.Find("UI/Canvas/Panel_MainMenu").GetComponent<MainMenuViewPresenter>();
        _mainMenuView.Show(false);

        _buttonKeysView = GameObject.Find("UI/Canvas/Panel_ButtonKeys").GetComponent<ButtonKeysViewPresenter>();
        _buttonKeysView.Show(false);

        _mainMenuView.StartButton.Clicked += StartGame;
        _mainMenuView.ExitButton.Clicked += Exit;
        _mainMenuView.KeysButton.Clicked += Keys;
        _buttonKeysView.CloseButton.Clicked += CloseKeys;

        loadManager = (LoadManager)GameObject.Find("LoadManager").GetComponent(typeof(LoadManager));

    }

    public void ShowMainMenu(bool show)
    {
        _mainMenuView.Show(show);
    }

    public void ShowKeys(bool show)
    {
        _buttonKeysView.Show(show);
    }

    private void StartGame(object sender, EventArgs args)
    {
        loadManager.LoadGame();
    }

    private void Exit(object sender, EventArgs args)
    {
        Application.Quit();
    }

    private void Keys(object sender, EventArgs args)
    {
        ShowKeys(true);
    }

    private void CloseKeys(object sender, EventArgs args)
    {
        ShowKeys(false);
    }

}
