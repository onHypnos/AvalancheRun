using System.Collections;
using UnityEngine;


public class CollectableView : BaseObjectView
{
    [SerializeField] public Rigidbody Rigidbody { get; private set; }
    [SerializeField] private int _value = 100;
    private static CollectableController _controller;
    public bool IsCollected = false;
    private bool _CanTouchDelay = false;
    public bool ColliderIsTrigger = false;
    public int Value => _value;
    public void Start()
    {
        if (Rigidbody == null)
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
        FindMyController();
        Invoke("GetSpawnDelay", 0.2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_CanTouchDelay)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameEvents.current.AddMoney(Value);
                GameObject.Destroy(this.gameObject, 0f);
                Debug.Log($"Added {Value} dollars(why?)");
                SetCollected();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_CanTouchDelay)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameEvents.current.AddMoney(Value);
                GameObject.Destroy(this.gameObject, 0f);
                SetCollected();
                Debug.Log($"Added {Value} dollars ");
            }
        }
    }
    
    public void FindMyController()
    {
        if (_controller == null)
        {
            _controller = FindObjectOfType<MainController>().GetController<CollectableController>();
        }
        _controller.AddView(this);
    }

    public void SetCollected(float time)
    {
        Invoke("CollectedStateTrue", time);
    }
    public void SetCollected()
    {
        SetCollected(0f);
    }

    private void CollectedState(bool value)
    {
        IsCollected = value;
    }
    private void CollectedStateTrue()
    {
        CollectedState(true);
    }

    private void GetSpawnDelay()
    {
        Rigidbody.AddForce(Vector3.up * 7, ForceMode.Impulse);
        _CanTouchDelay = true;
    }
    private void OnDestroy()
    {
        _controller.RemoveView(this);
    }
    public void SetColliderTrigger()
    {

        GetComponent<Collider>().isTrigger = true;
    }
}
