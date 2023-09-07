using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    Idle,
    Moving,
    Paused,
    Finished
}

public class GameManager : GenericSingleton<GameManager>
{

    internal PlayerMovement _player;
    private bool _readyForInput;
    private int currLvl;
    public LevelGenerator levelBuilder;
    public SpawnedElements spawnedElements;
    public GameState gameState;


    [SerializeField] internal float moveSpeed = .5f;

    private void Awake()
    {
        base.Awake();

        levelBuilder = FindObjectOfType<LevelGenerator>();
        spawnedElements = FindObjectOfType<SpawnedElements>();
    }

    private void Start()
    {
        //ResetScene();
        StartCoroutine(ResetSceneAsync());
    }

    internal bool AllowInput() => gameState == GameState.Idle;

    internal void ChangeGameState(GameState state)
    {
        gameState = state;
    }


    public void NextLevel()
    {
        levelBuilder.NextLevel();
        StartCoroutine(ResetSceneAsync());
    }
    public void ResetScene()
    {
        foreach(var box in spawnedElements.boxes)
        {
            box.gameobject.transform.position = box.initPos;
        }

        spawnedElements.player.gameobject.transform.position = spawnedElements.player.initPos;
    }

    public void Quit()
    {
        Application.Quit();
    }

    internal bool IsLevelComplete()
    {
        foreach (var box in spawnedElements.boxes)
        {
            Box temp = box.obj as Box;

            if (!temp.arrived)
            {
                return false;
            }
        }

        EventManager.Instance.onLevelComplete?.Invoke();
        return true;
    }

    IEnumerator ResetSceneAsync()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncUnload.isDone)
            {
                yield return null;
                Debug.Log("Unloading scene...");
            }
            Debug.Log("Unload Done!");
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Loading scene...");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        Debug.Log("Build level");
        levelBuilder.BuildLevel();
        _player = FindObjectOfType<PlayerMovement>();
    }
}