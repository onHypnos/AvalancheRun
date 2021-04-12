using System.Collections;
using UnityEngine;


public class ObjectSpawnerView : BaseObjectView
{
    [SerializeField] private FallingObjectsCSO _objects;
    [SerializeField] private FallingObjectsCSO[] _objectsPacks = new FallingObjectsCSO[5];

    private ObjectSpawnerController _controller;

    public FallingObjectsCSO ObjectList => _objects;
    public FallingObjectsCSO[] ObjectsPacks => _objectsPacks;


    public void Awake()
    {        
        FindMyController();
    }

    private void FindMyController()
    {
        _controller = FindObjectOfType<MainController>().GetController<ObjectSpawnerController>();
        if (_controller != null)
        {            
            _controller.AddSpawnerToList(this);
            
        }
        else 
        {
            Debug.LogWarning($"{this.gameObject.name} didn't find controller");
        }
    }

    private void OnDestroy()
    {
        _controller.RemoveSpawnerFromList(this);
    }
}