using UnityEngine;

public class PlayerView : BaseObjectView
{
    #region Fields
    [SerializeField] private PlayerState _state = PlayerState.Idle;
    [SerializeField, Range(1, 5)] private float _warpingCooldown = 2.0f;
    [SerializeField, Range(2, 8)] private float _baseWarpingDistance = 4.0f;
    [SerializeField, Range(5f, 10f)] private float _movementSpeed = 5.0f;
    [SerializeField] private float _baseAttackCooldown = 1f;    
    [SerializeField] private Animator _animator;
    [SerializeField] private int _attackAmount = 3;
    [SerializeField] private Rigidbody _playerRigidbody;
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
   
    public Animator Animator => _animator;
    public int AttackAmount => _attackAmount;    
    public bool AttackReady => _attackReady;

    public Rigidbody Rigidbody => _playerRigidbody;

    #endregion
    public void Start()
    {
        if (_playerRigidbody == null)
            _playerRigidbody = GetComponent<Rigidbody>();

        SetRagdoll(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameEvents.current.OnPlayerGetHit();
        }
    }

    public void OnDestroy()
    {
        
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

    public void SetRagdoll(bool value)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody body in bodies)
        {
            body.isKinematic = !value;
            body.transform.GetComponent<Collider>().enabled = value;
        }

        GetComponent<CapsuleCollider>().enabled = !value;
        _playerRigidbody.isKinematic = true;
        _animator.enabled = !value;
    }
}