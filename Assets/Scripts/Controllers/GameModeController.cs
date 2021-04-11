using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameModeController : BaseController, IExecute
{
    #region Fields
    private GameMode _gameMode;
    #endregion
    #region Acсess Modifyers
    public GameMode GameMode => _gameMode;
    #endregion

    private Dictionary<string, bool> _scenesList = new Dictionary<string, bool>();
    public GameModeController(MainController main) : base(main)
    {
        GameEvents.current.OnLevelStart += StartGame;
        GameEvents.current.OnGamePaused += PauseGame;
        GameEvents.current.OnGameResumed += ResumeGame;
    }

    

    public override void Execute()
    {
        base.Execute();
        if (!IsActive)
        {
            return;
        }
    }

    public void LoadAsyncScene(string sceneName)
    {
        if (_scenesList.ContainsKey(sceneName))
        {
            if (_scenesList[sceneName] == false)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                Debug.Log($"Scene {sceneName} was loaded");
                _scenesList[sceneName] = true;
            }
            else
            {
                Debug.LogError("Scene already loaded and can't be loaded again");
            }
        }
        else
        {
            AddSceneToList(sceneName);
            LoadAsyncScene(sceneName);
        }
    }
    
    public void UnloadAsyncScene(string sceneName)
    {
        if (_scenesList.ContainsKey(sceneName))
        {
            if (_scenesList[sceneName])
            {
                SceneManager.UnloadSceneAsync(sceneName);
                Debug.Log($"Scene {sceneName} was unloaded");
                _scenesList[sceneName] = true;
            }
        }
        else
        {
            AddSceneToList(sceneName);
        }
    }
    /// <summary>
    /// Add new scene in scene name
    /// </summary>
    /// <param name="sceneName"></param>
    private void AddSceneToList(string sceneName)
    {
        _scenesList.Add(sceneName, false);
        Debug.Log($"Scene {sceneName} was added in sceneList");
    }
    /// <summary>
    /// unload all loaded scenes from list and load sceneName scene
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadNewScene(string sceneName)
    {
        foreach (KeyValuePair<string, bool> entry in _scenesList)
        {            
            if (entry.Value)
            {
                UnloadAsyncScene(entry.Key);
            }
        }
        LoadAsyncScene(sceneName);
        GameEvents.current.SetActiveCamera("Up-to-up Virtual Camera");
        GameEvents.current.SceneChanged();
        Time.timeScale = 0;
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}