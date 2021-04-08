using System.Collections.Generic;
using UnityEngine;


public class ObjectSpawnerController : BaseController
{
    private GameObject _temp;
    private List<GameObject> _list = new List<GameObject>();
    public ObjectSpawnerController(MainController main) : base(main)
    {

    }

    public void SpawnObjectsPool(FallingObjectsCSO objects, float deltaTime)
    {
        ObjectSpawner.current.CreateObjectsInTime(objects.Objects, deltaTime);
    }

    public void SpawnObjectsPool(FallingObjectsCSO objects)
    {
        SpawnObjectsPool(objects, 0);
    }

    public void AddObjectToList(GameObject obj)
    {
        if (!_list.Contains(obj))
        {
            _list.Add(obj);
        }
        else
        {
            Debug.LogWarning($"{obj} already in list");
        }
    }

    public void RemoveObjectToList(GameObject obj)
    {
        if (_list.Contains(obj))
        {
            _list.Remove(obj);
        }
        else
        {
            Debug.LogWarning($"{obj} already not in list");
        }
    }
}