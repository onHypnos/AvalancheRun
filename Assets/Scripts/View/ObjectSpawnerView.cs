using System.Collections;
using UnityEngine;


public class ObjectSpawnerView : BaseObjectView
{
    private ObjectSpawnerController _controller;
    [SerializeField] private FallingObjectsCSO _objects;
    public FallingObjectsCSO ObjectList => _objects;
    private void Awake()
    {
        if (_controller != null)
        {
            FindMyController();
        }
    }

    private void FindMyController()
    {
        _controller = FindObjectOfType<MainController>().GetController<ObjectSpawnerController>();        
        if (_controller != null)
        {
            GameEvents.current.SetObjectSpawner(this);
        }
    }

    private void OnDestroy()
    {        
        GameEvents.current.OnDeleteObjectSpawner(this);
    }
}