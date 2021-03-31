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

    private int _warpRaycastLayerMask = 1;
    private RaycastHit _warpRaycastHit;
    private float _warpObstacleDelta = 0;

    private int _movementForwardRaycastLayerMask = 1;
    private RaycastHit _movementForwardRaycastHit;
    //private int _movementDownRaycastLayerMask = 1;
    //private RaycastHit _movementDownRaycastHit;
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

        _warpRaycastLayerMask = LayerMasksConsts.WarpCheckLayerMask;
        _movementForwardRaycastLayerMask = LayerMasksConsts.MovementRaycastLayerMask;
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
        InputEvents.current.OnDoubleTouchEvent += CheckWarping;
        InputEvents.current.OnTriggerWarpWall += OnTriggerWarpZoneEnter;
        InputEvents.current.OnTeleportEvent += WarpPlayer;
        GameEvents.current.OnPlayerCollideEnemy += OnPlayerCollideEnemy;
        GameEvents.current.OnPlayerGlideEvent += OnPlayerGlide;
        GameEvents.current.OnLevelEnd += PlayerWinningDance;
        GameEvents.current.OnSceneChanged += ResetPlayerState;
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
        DrawWarpRaycast();
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
                    CheckRunningForward();
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
    /// Called in frame where player gliding
    /// </summary>
    public void OnPlayerGlide()
    {
        _player.transform.Translate(Vector3.forward * Time.deltaTime);
    }
    /// <summary>
    /// Called on player have collision with enemy tagged gameobject
    /// </summary>
    /// <param name="enemy"></param>
    public void OnPlayerCollideEnemy(Collider enemy)
    {
        if (_player.AttackReady)
        {
            _player.Animator.SetTrigger($"Attack {UnityEngine.Random.Range(0, _player.AttackAmount)}");
            _player.SetAttackOnCooldown();
        }
    }
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
            SetPlayerState(PlayerState.Idle);
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
        SetPlayerState(PlayerState.Dead);
    }
    /// <summary>
    /// Warp Player if player can warping
    /// </summary>
    /// <param name="delta"></param>
    public void CheckWarping()
    {
        if (PlayerIsActive)
        {
            if (_player.CanWarping && _player.State != PlayerState.Dead)
            {
                WarpPlayer();
            }
        }
    }
    /// <summary>
    /// Called when player warp collider have collision with object that cannot been throwed
    /// </summary>
    /// <param name="warpObstacleDelta"></param>
    public void OnTriggerWarpZoneEnter(float warpObstacleDelta)
    {
        _warpObstacleDelta = warpObstacleDelta;
    }
    /// <summary>
    /// Check player's ability to run forward
    /// </summary>
    public bool CheckRunningForward()
    {
        if (Physics.Raycast(Transform.position + Vector3.up, Transform.TransformDirection(Vector3.forward),
            out _movementForwardRaycastHit, 0.3f, _movementForwardRaycastLayerMask))
        {
            Debug.DrawRay(Transform.position + Vector3.up, Transform.TransformDirection(Vector3.forward) * 0.3f, Color.blue);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Warp player to Warping location
    /// </summary>
    private void WarpPlayer()
    {
        if (PlayerIsActive)
        {
            _player.Animator.SetTrigger("Warping");
            _player.Position = _player.WarpZone.transform.position;
            _player.SetWarpingOnCD();
            GameEvents.current.PlayerWarpEvent();
        }
    }
    /// <summary>
    /// Drawing Raycast for Warp Distance and check hits
    /// </summary>
    private void DrawWarpRaycast()
    {
        _temp.x = 0f;
        _temp.y = 0f;
        if (Physics.Raycast(Transform.position + Vector3.up, Transform.TransformDirection(Vector3.forward),
            out _warpRaycastHit, _player.BaseWarpingDistance + 0.5f, _warpRaycastLayerMask))
        {
            Debug.DrawRay(Transform.position + Vector3.up, Transform.TransformDirection(Vector3.forward) * _warpRaycastHit.distance, Color.red);
            _temp.z = _warpRaycastHit.distance - _player.WarpZone.transform.localScale.z * 0.5f;
        }
        else
        {
            Debug.DrawRay(Transform.position + Vector3.up, Transform.TransformDirection(Vector3.forward) * (_player.BaseWarpingDistance + 0.5f), Color.white);
            _temp.z = _player.BaseWarpingDistance;// + _warpObstacleDelta;
        }
        _player.WarpZone.transform.localPosition = _temp;
        //исправить
        _player.WarpZoneColliderDelta.transform.localPosition = new Vector3(0, 0, _player.BaseWarpingDistance);

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