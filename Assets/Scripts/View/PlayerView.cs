using UnityEngine;

public class PlayerView : BaseObjectView
{
    #region Fields
    [SerializeField] private PlayerState _state = PlayerState.Idle;
    
    [SerializeField, Range(5f, 10f)] private float _movementSpeed = 5.0f;  
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] public GameObject _bombShield;
    [SerializeField] private bool _bombShieldReady = true;

    private float _animationBlend;
    private bool _attackReady = true;

    #endregion

    #region AccsessModifyers
    public bool BombShieldReady => _bombShieldReady;
    public float MovementSpeed => _movementSpeed;
    public float AnimationBlend => _animationBlend;
    
    public PlayerState State => _state;
   
    public Animator Animator => _animator;    
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
            Invoke("LevelFail", 1f);
        }
    }

    public void OnDestroy()
    {
        
    }

    public void SetState(PlayerState state)
    {
        _state = state;
    }

    public void ActivateBombShield()
    {
        _bombShieldReady = false;
        _bombShield.SetActive(true);
        StartCoroutine(Utilits.BombShieldBehavior(_bombShield));
    }
    
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

    private void LevelFail()
    {
        GameEvents.current.LevelFailed();
    }
}