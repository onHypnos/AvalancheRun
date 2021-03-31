using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    /// <summary>
    /// Called When Object with Tag "Enemy" Collide with Player
    /// </summary>
    public Action<Collider> OnPlayerCollideEnemy;
    public void PlayerCollideEnemy(Collider collider)
    {
        OnPlayerCollideEnemy?.Invoke(collider);
    }

    public Action OnPlayerGlideEvent;
    public void PlayerGlideEvent()
    {
        OnPlayerGlideEvent?.Invoke();
    }

    public Action OnPlayerWarpEvent;
    public void PlayerWarpEvent()
    {
        OnPlayerWarpEvent?.Invoke();
    }

    public Action<EnemyView> OnEnemyInWarpZoneCollider;
    public void EnemyInWarpZoneCollider(EnemyView enemy)
    {
        OnEnemyInWarpZoneCollider?.Invoke(enemy);
    }

    public Action<EnemyView> OnEnemyLeaveWarpZoneCollider;
    public void EnemyLeaveWarpZoneCollider(EnemyView enemy)
    {
        OnEnemyLeaveWarpZoneCollider?.Invoke(enemy);
    }

    public Action<EnemyView> OnEnemyGetDamage;
    public void EnemyGetDamage(EnemyView enemy)
    {
        OnEnemyGetDamage?.Invoke(enemy);
    }


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

    public Action<EnemyView> OnEnemyKilled;
    public void EnemyKilled(EnemyView enemy)
    {
        OnEnemyKilled?.Invoke(enemy);
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
    /// <summary>
    /// when added new scene to other
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

    public Action OnLevelEnd;
    public void LevelEnd()
    {
        OnLevelEnd?.Invoke();
    }

    public Action<string> OnSettingActiveCamera;
    public void SetActiveCamera(string cameraName)
    {
        OnSettingActiveCamera?.Invoke(cameraName);
    }
}