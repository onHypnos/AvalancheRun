using System.Collections;
using UnityEngine;


public class ObjectSpawnerView : BaseObjectView
{
    private ObjectSpawnerController _controller;
    [SerializeField] private FallingObjectsCSO _objects;
    public FallingObjectsCSO ObjectList => _objects;
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