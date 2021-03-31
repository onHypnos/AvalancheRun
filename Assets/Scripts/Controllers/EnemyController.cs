using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController, IExecute
{
    protected PlayerView _player;
    private List<EnemyView> _enemyList = new List<EnemyView>();
    private Dictionary<EnemyStates, IEnemyState> _stateList = new Dictionary<EnemyStates, IEnemyState>();
    public EnemyController(MainController main) : base(main) 
    {
        _stateList.Add(EnemyStates.Idle, new EnemyIdleStateModel());
        _stateList.Add(EnemyStates.Downed, new EnemyDownedStateModel());
        _stateList.Add(EnemyStates.Dead, new EnemyDeadStateModel());
        _stateList.Add(EnemyStates.Allert, new EnemyAllertStateModel());
    }

    public PlayerView Player => _player;
    public override void Initialize()
    {
        base.Initialize();
        _player = Main.GetController<PlayerController>().GetPlayer;
        GameEvents.current.OnEnemyInWarpZoneCollider += EnableEnemyVisualTrigger;
        GameEvents.current.OnEnemyLeaveWarpZoneCollider += DisableEnemyVisualTrigger;
        GameEvents.current.OnEnemyGetDamage += KillEnemy;
    }
    public override void Execute()
    {
        base.Execute();
        foreach (EnemyView enemy in _enemyList)
        {
            if (enemy == null)
            {
                RemoveEnemyFromList(enemy);
            }
            else
            {
                _stateList[enemy.State].Execute(enemy, this);
            }
        }
    }

    public bool PlayerInFieldOfView(EnemyView enemy,PlayerView player)
    {
        if ((player.Position - enemy.Position).magnitude < enemy.DistanceOfView)
        {
            if (Vector3.Angle((enemy.transform.forward).normalized, (player.Transform.position - enemy.Position).normalized) < enemy.FieldOfView)
            {
                return true;
            }
        }
        return false;
    }
    
    public void SetEnemyState(EnemyView enemy, EnemyStates state)
    {
        enemy.SetState(state);
    }
    /// <summary>
    /// Add enemy to list 
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemyToList(EnemyView enemy)
    {
        if (!_enemyList.Contains(enemy))
        {
            _enemyList.Add(enemy);
            SetEnemyState(enemy, EnemyStates.Idle);
        }
    }
    /// <summary>
    /// Remove enemy from list
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveEnemyFromList(EnemyView enemy)
    {
        if (_enemyList.Contains(enemy))
        {
            _enemyList.Remove(enemy);
        }
    }
    public void EnableEnemyVisualTrigger(EnemyView enemy)
    {
        if (_enemyList.Contains(enemy))
        {
            enemy.EnableVisualTrigger();
        }
    }
    public void DisableEnemyVisualTrigger(EnemyView enemy)
    {
        if (_enemyList.Contains(enemy))
        {
            enemy.DisableVisualTrigger();
        }
    }
    public void KillEnemy(EnemyView enemy)
    {
        if (_enemyList.Contains(enemy))
        {
            enemy.Animator.enabled = false;
            enemy.RagdollState(true);
            SetEnemyState(enemy, EnemyStates.Dead);
            enemy.tag = "DeadEnemy";
            GameEvents.current.EnemyKilled(enemy);
        }
    }
}