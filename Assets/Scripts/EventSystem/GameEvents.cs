using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }


    #region General events
    /// <summary>
    /// When added new scene to other
    /// </summary>
    public Action OnSceneLoad;
    public void SceneLoad()
    {
        OnSceneLoad?.Invoke();
    }

    /// <summary>
    /// when new level loaded
    /// </summary>
    public Action OnSceneChanged;
    public void SceneChanged()
    {
        OnSceneChanged?.Invoke();
    }

    public Action OnLevelStart;
    public void LevelStart()
    {
        OnLevelStart?.Invoke();
    }

    public Action OnLevelEnd;
    public void LevelEnd()
    {
        OnLevelEnd?.Invoke();
    }

    public Action OnLevelComplete;
    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public Action OnLevelFailed;
    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }

    public Action OnGamePaused;
    public void GamePaused()
    {
        OnGamePaused?.Invoke();
    }

    public Action OnGameResumed;
    public void GameResumed()
    {
        OnGameResumed?.Invoke();
    }

    public Action OnLevelRestart;
    public void LevelRestart()
    {
        OnLevelRestart?.Invoke();
    }
    #endregion

    #region Currency events
    public Action<int> OnAddMoney;
    public void AddMoney(int value)
    {
        OnAddMoney?.Invoke(value);
    }
    public Action<int> OnRemoveMoney;
    public void RemoveMoney(int value)
    {
        OnRemoveMoney?.Invoke(value);
    }
    #endregion

    #region Camera events
    public Action<string> OnSettingActiveCamera;
    public void SetActiveCamera(string cameraName)
    {
        OnSettingActiveCamera?.Invoke(cameraName);
    }
    #endregion

    #region Player events
    public Action OnPlayerGetHit;
    public void PlayerGetHit()
    {
        OnPlayerGetHit?.Invoke();
    }

    /// <summary>
    /// Called When Object with Tag "Enemy" Collide with Player
    /// </summary>
    public Action<Collider> OnPlayerCollideEnemy;
    public void PlayerCollideEnemy(Collider collider)
    {
        OnPlayerCollideEnemy?.Invoke(collider);
    }

    public Action OnPlayerAttackStart;
    public void PlayerAttackStart()
    {
        OnPlayerAttackStart?.Invoke();
    }

    public Action OnPlayerAttackEnd;
    public void PlayerAttackEnd()
    {
        OnPlayerAttackEnd?.Invoke();
    }
    #endregion

    #region Enemy events
    public Action<EnemyView> OnEnemyGetDamage;
    public void EnemyGetDamage(EnemyView enemy)
    {
        OnEnemyGetDamage?.Invoke(enemy);
    }

    public Action<EnemyView> OnEnemyKilled;
    public void EnemyKilled(EnemyView enemy)
    {
        OnEnemyKilled?.Invoke(enemy);
    }

    public Action<EnemyView> OnMemberFinish;
    public void MemberFinish(EnemyView enemy)
    {
        OnMemberFinish?.Invoke(enemy);
    }
    #endregion

    #region Spawner events
    public Action<GameObject> OnCreatingNewFallingItem;
    public void CreatingNewFallingItem(GameObject obj)
    {
        OnCreatingNewFallingItem?.Invoke(obj);
    }

    public Action<ObjectSpawnerView> OnSetObjectSpawner;
    public void SetObjectSpawner(ObjectSpawnerView view)
    {
        OnSetObjectSpawner?.Invoke(view);
    }


    public Action<ObjectSpawnerView> OnDeleteObjectSpawner;
    public void DeleteObjectSpawner(ObjectSpawnerView view)
    {
        OnDeleteObjectSpawner?.Invoke(view);
    }
    #endregion
}