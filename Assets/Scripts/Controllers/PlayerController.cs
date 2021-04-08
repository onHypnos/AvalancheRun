using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController, IExecute
{
    #region Fields
    private IPlayerState state;
    private PlayerView _player;
    private Dictionary<PlayerState, IPlayerState> _stateList = new Dictionary<PlayerState, IPlayerState>();
    public bool PlayerIsActive { get; private set; }

    private Vector2 _positionDelta = Vector2.zero;
    private Vector2 _positionBegan = Vector2.zero;
    private Vector3 _temp;
    
    
    #endregion
    #region Access Modifyers
    public Vector2 PositionDelta => _positionDelta;
    public Vector2 PositionBegan => _positionBegan;
    public Transform Transform => _player.Transform;
    public PlayerView GetPlayer => _player;

    #endregion
    #region Construsctors
    public PlayerController(MainController main) : base(main)
    {
        _stateList.Add(PlayerState.Idle, new PlayerIdleStateModel());
        _stateList.Add(PlayerState.Moving, new PlayerMovingStateModel());
        _stateList.Add(PlayerState.Dead, new PlayerDeadStateModel());
    }

    #endregion
    #region IInitialize
    public override void Initialize()
    {
        base.Initialize();
        PlayerIsActive = true;
        InputEvents.current.OnTouchBeganEvent += SetBeganPosition;
        InputEvents.current.OnTouchMovedEvent += SetMoving;
        //GameEvents.current.OnTouchStationaryEvent += SetIdle;
        InputEvents.current.OnTouchEndedEvent += SetIdle;
        InputEvents.current.OnTouchCancelledEvent += SetIdle;       
        
        GameEvents.current.OnLevelEnd += PlayerWinningDance;
        GameEvents.current.OnSceneChanged += ResetPlayerState;
        GameEvents.current.OnPlayerGetHit += SetDead;
        InputEvents.current.OnDoubleTouchEvent += OnDoubleTouchEvent;
    }
    #endregion
    #region IExecute
    public override void Execute()
    {
        base.Execute();
        if (!IsActive)
        {
            return;
        }
        
        switch (_player.State)
        {
            case PlayerState.Idle:
                {
                    state = _stateList[PlayerState.Idle];
                    break;
                }
            case PlayerState.Moving:
                {
                    state = _stateList[PlayerState.Moving];                    
                    break;
                }
            case PlayerState.Dead:
                {
                    state = _stateList[PlayerState.Dead];
                    break;
                }
        }
        if (PlayerIsActive)
        {
            state.Execute(this, _player);
        }
    }
    #endregion
    /// <summary>
    /// Set player instance if it wasn't setted
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(PlayerView player)
    {
        if (_player == null && player != null)
        {
            _player = player;
        }
    }
    /// <summary>
    /// Set player state to State
    /// </summary>
    /// <param State="state"></param>
    public void SetPlayerState(PlayerState state)
    {
        _player.SetState(state);
    }
    /// <summary>
    /// Set _began position of player
    /// </summary>
    /// <param name="position"></param>
    public void SetBeganPosition(Vector2 position)
    {
        if (PlayerIsActive)
        {
            _positionBegan = position;
        }
    }
    /// <summary>
    /// Set PlayerState Idle
    /// </summary>
    public void SetIdle()
    {
        if (PlayerIsActive)
        {
            SetPlayerState(PlayerState.Idle);
        }
    }
    /// <summary>
    /// Set Player state to Moving state and get next input Delta position
    /// </summary>
    /// <param name="delta"></param>
    public void SetMoving(Vector2 delta)
    {
        if (PlayerIsActive)
        {
            SetPlayerState(PlayerState.Moving);
            _positionDelta = delta;
        }
    }
    /// <summary>
    /// Set player's state to Dead state
    /// </summary>
    public void SetDead()
    {
        if (PlayerIsActive)
        {
            SetPlayerState(PlayerState.Dead);
            _player.SetRagdoll(true);
            SetState(false);
        }
    }     


    public void OnDoubleTouchEvent()
    {
        if (_player.BombShieldReady)
        {
            _player.ActivateBombShield();
        }
    }

    public void ResetPlayerState()
    {
        ResetPlayerAnimatorState();
        Transform.position = GameObject.FindObjectOfType<PlayerStarterPosition>().transform.position;
        PlayerIsActive = true;
    }

    public void ResetPlayerAnimatorState()
    {
        _player.Animator.SetTrigger("Reset");
    }

    public void PlayerWinningDance()
    {
        _player.Rotation = Quaternion.LookRotation(Vector3.forward * -1f, Vector3.up);
        _player.Animator.SetTrigger($"Dance {Random.Range(0, 6)}");
        GameEvents.current.SetActiveCamera("Dancing Camera");
        PlayerIsActive = false;
    }
}