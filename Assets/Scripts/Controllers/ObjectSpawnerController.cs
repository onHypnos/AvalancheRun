using System.Collections.Generic;
using UnityEngine;


public class ObjectSpawnerController : BaseController
{
    private GameObject _temp;
    private List<ObjectSpawnerView> _spawners = new List<ObjectSpawnerView>();
    public ObjectSpawnerController(MainController main) : base(main)
    {

    }

    public override void Initialize()
    {
        base.Initialize();        
        GameEvents.current.OnLevelStart += CallSpawners;
    }

    public void CallSpawners()
    {
        if (_spawners.Count > 0)
        {
            foreach (ObjectSpawnerView view in _spawners)
            {                
                SpawnObjectsPool(view, view.ObjectList, 0.35f);
            }
        }
        else
        {
            Debug.LogWarning($"ObjectSpawnerController Haven't spawners available");
        }
    }

    private void SpawnObjectsPool(ObjectSpawnerView view, FallingObjectsCSO objects, float deltaTime)
    {
        
        ObjectSpawner.current.CreateObjectsInTime(objects.Objects, view, deltaTime);
    }

    private void SpawnObjectsPool(ObjectSpawnerView view)
    {
        SpawnObjectsPool(view, view.ObjectList, 0);
    }

    public void AddSpawnerToList(ObjectSpawnerView view)
    {
        if (!_spawners.Contains(view))
        {
            _spawners.Add(view);
            Debug.Log($"{view.gameObject.name} was added in list");
        }
        else
        {
            Debug.LogWarning($"{view.gameObject.name} already in list");
        }
    }

    public void RemoveSpawnerFromList(ObjectSpawnerView view)
    {
        if (_spawners.Contains(view))
        {
            _spawners.Remove(view);
        }
        else
        {
            Debug.LogWarning($"{view.gameObject.name} already not in list");
        }
    }
}