using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : BaseObjectView
{
    private static EnemyController _controller;

    #region Fields
    private bool _dead = false;
    private EnemyStates _state = EnemyStates.Idle;
    private Rigidbody[] _ragBones;
    private Collider[] _colliders;
    [SerializeField,Range(30f,90f)] private float _fieldOfView = 90.0f;
    [SerializeField, Range(3f,15f)] private float _distanceOfView = 3f;
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _visualTrigger;
    [SerializeField] private GameObject _visualAllertTrigger;
    #endregion
    #region Access modifiers
    public Animator Animator => _animator;
    public EnemyStates State => _state;
    public bool IsDead => _dead;
    public float FieldOfView => _fieldOfView;
    public float DistanceOfView => _distanceOfView;
    #endregion


    public void CheckDead()
    {
        if (IsDead)
        { 
            if (gameObject.tag == "Enemy")
            {
                gameObject.tag = "DeadEnemy";
            }
        }
    }
    public void Awake()
    {        
        RagdollState(false);
    }
    public void Start()
    {
        FindMyController();
    }
    public void SetState(EnemyStates state)
    {
        _state = state;
    }
    public void FindMyController()
    {
        if (_controller == null)
        {
            _controller = FindObjectOfType<MainController>().GetController<EnemyController>();
        }
        _controller.AddEnemyToList(this);
    }
    #region Visual Trigger
    public void EnableVisualTrigger()
    {
        if (_visualTrigger != null)
        {
            if (!_visualTrigger.activeSelf)
            {
                _visualTrigger.SetActive(true);
            }
        }
    }
    public void DisableVisualTrigger()
    {
        if (_visualTrigger != null)
        {
            if (_visualTrigger.activeSelf)
            {
                _visualTrigger.SetActive(false);
            }
        }
    }
    public void RotateVisualTrigger()
    {
        if (_visualTrigger.activeSelf)
        { 
            _visualTrigger.transform.Rotate(0, 1, 0);        
        }
    }
    #endregion
    #region Visual Alert Trigger
    public void EnableAllertTrigger()
    {
        if (_visualAllertTrigger != null)
        { 
            if(!_visualAllertTrigger.activeSelf)
            {
                _visualAllertTrigger.SetActive(true);
            }
        }
    }
    public void DisableAllertTrigger()
    {
        if (_visualAllertTrigger != null)
        {
            if (_visualAllertTrigger.activeSelf)
            {
                _visualAllertTrigger.SetActive(false);
            }
        }
    }
#endregion
    public void OnTriggerEnter(Collider other)
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WarpZoneCollider"))
        {
            GameEvents.current.EnemyInWarpZoneCollider(this);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WarpZoneCollider"))
        {
            GameEvents.current.EnemyLeaveWarpZoneCollider(this);
        }
    }
    public void RagdollState(bool state)
    {        
        _ragBones = GetComponentsInChildren<Rigidbody>();
        foreach (var bone in _ragBones)
        {
            bone.isKinematic = !state;
        }
        _colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
    }
}