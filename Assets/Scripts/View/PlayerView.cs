using UnityEngine;

public class PlayerView : BaseObjectView
{
    #region Fields
    [SerializeField] private PlayerState _state = PlayerState.Idle;
    [SerializeField, Range(1, 5)] private float _warpingCooldown = 2.0f;
    [SerializeField, Range(2, 8)] private float _baseWarpingDistance = 4.0f;
    [SerializeField, Range(5f, 10f)] private float _movementSpeed = 5.0f;
    [SerializeField] private float _baseAttackCooldown = 1f;
    [SerializeField] private GameObject _warpZone;
    [SerializeField] private GameObject _warpZoneColliderDelta;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _attackAmount = 3;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _rightLegTrail;
    [SerializeField] private GameObject _leftLegTrail;
    [SerializeField] public GameObject AttackingCollider;
    private float _animationBlend;
    private bool _canWarping = true;
    private bool _attackReady = true;

    #endregion

    #region AccsessModifyers
    public float MovementSpeed => _movementSpeed;
    public float AnimationBlend => _animationBlend;
    public bool CanWarping => _canWarping;
    public float BaseWarpingDistance => _baseWarpingDistance;
    public PlayerState State => _state;
    public GameObject WarpZone => _warpZone;
    public GameObject WarpZoneColliderDelta => _warpZoneColliderDelta;
    public Animator Animator => _animator;
    public int AttackAmount => _attackAmount;
    public GameObject RightHand => _rightHand;
    public GameObject LeftLegTrail => _leftLegTrail;
    public GameObject RightLegTrail => _rightLegTrail;
    public bool AttackReady => _attackReady;

    #endregion
    public void Start()
    {
        GameEvents.current.OnPlayerAttackStart += OnPlayerAttackStartEvent;
        GameEvents.current.OnPlayerAttackEnd += OnPlayerAttackEndEvent;
    }

    public void OnDestroy()
    {
        GameEvents.current.OnPlayerAttackStart -= OnPlayerAttackStartEvent;
        GameEvents.current.OnPlayerAttackEnd -= OnPlayerAttackEndEvent;
    }

    private void OnPlayerAttackStartEvent()
    {
        AttackingCollider?.SetActive(true);
        _rightLegTrail?.SetActive(true);
        _leftLegTrail?.SetActive(true);
    }
    private void OnPlayerAttackEndEvent()
    {
        AttackingCollider?.SetActive(false);
        _rightLegTrail?.SetActive(false);
        _leftLegTrail?.SetActive(false);
    }

    public void SetState(PlayerState state)
    {
        _state = state;
    }
    public void SetWarpingOnCD()
    {
        _canWarping = false;
        Invoke("WarpingIsReady", _warpingCooldown);
    }
    private void WarpingIsReady()
    {
        _canWarping = true;
    }

    #region SetAttackReady
    public void SetAttackOnCooldown()
    {
        _attackReady = false;
        Invoke("InvokedSetAttackReady", _baseAttackCooldown);
    }
    private void InvokedSetAttackReady()
    {
        _attackReady = true;
    }
    #endregion


}