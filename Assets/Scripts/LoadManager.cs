using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
	private bool isGameOver;
	private bool isPaused;
	private bool isBoss = false;
	private bool isCutscene = false;
	private bool isRestart = false;

	private const string SCENE_MAINMENU = "MainMenu";
    private const string SCENE_GAME = "Game";
    private const string SCENE_GAMEPLAY = "GamePlay";

    private LoadScreenViewPresenter _loadScreenView;

    private static LoadManager _instance = null;
    public static LoadManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("LoadManager").AddComponent<LoadManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _loadScreenView = GameObject.Find("UI/Canvas/Panel_LoadScreen").GetComponent<LoadScreenViewPresenter>();
        _loadScreenView.Show(false);

        // start of game
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
		stopAllAudio();
        // show load screen
        _loadScreenView.Show(true);

        StartCoroutine(LoadMainMenuAsync());
    }

    private IEnumerator LoadMainMenuAsync()
    {

        // make sure to unload all scenes before
        yield return StartCoroutine(UnloadScene(SCENE_MAINMENU));
        yield return StartCoroutine(UnloadScene(SCENE_GAMEPLAY));
        yield return StartCoroutine(UnloadScene(SCENE_GAME));
        yield return StartCoroutine(CleanupUnusedAssets());

        if (!IsSceneLoaded(SCENE_MAINMENU))
        {
            yield return StartCoroutine(LoadScene(SCENE_MAINMENU));
        }

        // hide load screen when done loading scene
        _loadScreenView.Show(false);

    }

    public void LoadGame()
    {
        _loadScreenView.Show(true);

		isGameOver = false;
		isPaused = false;

        StartCoroutine(LoadGameAsync());

    }

    private IEnumerator LoadGameAsync()
    { 
        yield return StartCoroutine(UnloadScene(SCENE_MAINMENU));
        yield return StartCoroutine(UnloadScene(SCENE_GAMEPLAY));
        yield return StartCoroutine(UnloadScene(SCENE_GAME));
        yield return StartCoroutine(CleanupUnusedAssets());

        if (!IsSceneLoaded(SCENE_GAMEPLAY))
        {
            yield return StartCoroutine(LoadScene(SCENE_GAMEPLAY));
        }

        if (!IsSceneLoaded(SCENE_GAME))
        {
            yield return StartCoroutine(LoadScene(SCENE_GAME));
        }

        _loadScreenView.Show(false);
    }

    private IEnumerator CleanupUnusedAssets()
    {
        AsyncOperation unloadAsync = Resources.UnloadUnusedAssets();
        while (!unloadAsync.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator UnloadScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }
    }

    /*
    public GameObject LoadGameObject(string path, Vector3 pos, Quaternion rot)
    {
        GameObject g = (GameObject)Instantiate(Resources.Load<GameObject>(path), pos, rot);
        return g;
    }
    */

    private bool IsSceneLoaded(string sceneName)
    {
        GameObject sceneAsset = GameObject.Find(sceneName);
        return (sceneAsset != null);
    }

	public bool getGameOver() {
		return isGameOver;
	}
	public void setGameOver(bool go) {
		isGameOver = go;
	}

	public bool getIsPaused() {
		return isPaused;
	}
	public void setIsPaused(bool ip) {
		isPaused = ip;
	}
	public bool getIsBoss() {
		return isBoss;
	}
	public void setIsBoss(bool ib) {
		isBoss = ib;
	}
	public bool getIsCutscene() {
		return isCutscene;
	}
	public void setIsCutscene(bool ic) {
		isCutscene = ic;
	}
	public bool getIsRestart() {
		return isRestart;
	}
	public void setIsRestart(bool ir) {
		isRestart = ir;
	}
	void stopAllAudio() {
		AudioSource[] allAudio = FindObjectsOfType<AudioSource> ();
		foreach (AudioSource a in allAudio) {
			a.Stop();
		}
	}
}