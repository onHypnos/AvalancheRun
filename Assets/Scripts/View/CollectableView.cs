using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CollectableView : BaseObjectView
{
    [SerializeField] private int _value = 100;
    private static CollectableController _controller;


    public void Start()
    {
        transform.position = 
            new Vector3(transform.position.x + Random.Range(-3f, 3f), transform.position.y, transform.position.z);
        FindMyController();
        GetComponent<Collider>().isTrigger = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.Player))
        {
            GameEvents.Current.AddMoney(_value);
            Destroy(gameObject, 0f);
#if UNITY_EDITOR
            Debug.Log($"Added {_value} dollars ");
#endif
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

    private void OnDestroy()
    {
        _controller.RemoveView(this);
    }
}
