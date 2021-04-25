using System;
using System.Collections.Generic;
using UnityEngine;


public class ObjectSpawnerController : BaseController, IExecute
{
    private GameObject _temp;
    private List<ObjectSpawnerView> _spawners = new List<ObjectSpawnerView>();
    private Dictionary<Rigidbody, BodyInfo> _currentSceneObjects = new Dictionary<Rigidbody, BodyInfo>();
    private bool _slowTime = false;
    private float _slowTimeFactor = 0.15f;

    private SaveDataRepo _saveData;
    private int _difficulty;
    private int _maxDifficulty = 3;
    private int _minDifficulty = 0;

    #region TemporalFields
    private Vector3 _acceleration;
    private Vector3 _angularAcceleration;
    private Rigidbody _temporalRig;
    #endregion
    public ObjectSpawnerController(MainController main) : base(main)
    {
        _saveData = new SaveDataRepo();
        _difficulty = Mathf.Clamp(_saveData.LoadInt(SaveKeyManager.Difficulty),_minDifficulty,_maxDifficulty);        
    }

    public override void Initialize()
    {
        base.Initialize();
        GameEvents.Current.OnLevelStart += CallSpawners;
        GameEvents.Current.OnAddFallingObject += AddFallingObjectToDict;
        GameEvents.Current.OnStartSlowMode += OnSlowModeStart;
        GameEvents.Current.OnEndingSlowMode += OnSlowModeEnd;
        //GameEvents.current.OnLevelStart += ClearFallingObjectsList;
        GameEvents.Current.OnLevelEnd += OnLevelEnd;
        GameEvents.Current.OnLevelComplete += IncreaseDifficulty;
        GameEvents.Current.OnLevelFailed += ReduceDifficulty;
    }

    public override void Execute()
    {
        base.Execute();
        if (_slowTime)
        {
            SlowObjects();
        }
    }

    private void SlowObjects()
    {
        foreach (KeyValuePair<Rigidbody, BodyInfo> item in _currentSceneObjects)
        {
            if (item.Value.PrevVelocity != null)
            {
                //calc acceleration
                _acceleration = item.Key.velocity - item.Value.PrevVelocity.Value;

                //calc angular acceleration
                _angularAcceleration = item.Key.angularVelocity - item.Value.PrevAngularVelocity.Value;

                //assign new velocity
                item.Value.PrevVelocity = item.Key.velocity = item.Value.UnscaledVelocity * _slowTimeFactor;
                item.Value.PrevAngularVelocity = item.Key.angularVelocity = item.Value.UnscaledAngularVelocity * _slowTimeFactor;

                //assign acceleration
                item.Value.UnscaledVelocity += _acceleration;
                item.Value.UnscaledAngularVelocity += _angularAcceleration;
            }
            else
            {
                item.Value.UnscaledVelocity = item.Key.velocity;
                item.Value.UnscaledAngularVelocity = item.Key.angularVelocity;
                //first step
                item.Value.PrevVelocity = item.Key.velocity = item.Value.UnscaledVelocity * _slowTimeFactor;
                item.Value.PrevAngularVelocity = item.Key.angularVelocity = item.Value.UnscaledAngularVelocity * _slowTimeFactor;
                
            }
        }
    }

    private void OnSlowModeStart()
    {
        _slowTime = true;
    }
    private void OnSlowModeEnd()
    {
        foreach (KeyValuePair<Rigidbody, BodyInfo> item in _currentSceneObjects)
        {
            item.Key.velocity = item.Value.UnscaledVelocity;
            item.Key.angularVelocity = item.Value.UnscaledAngularVelocity;
        }
        _slowTime = false;
    }

    public void CallSpawners()
    {
        _slowTime = false;
        ClearFallingObjectsList();
        if (_spawners.Count > 0)
        {
            foreach (ObjectSpawnerView view in _spawners)
            {
                if (view != null)
                    SpawnObjectsPool(view, view.ObjectsPacks[_difficulty], 0.8f-_difficulty*0.12f);
                else
                    RemoveSpawnerFromList(view);
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"ObjectSpawnerController Haven't spawners available");
#endif
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

    private void AddFallingObjectToDict(GameObject obj)
    {
        _temporalRig = obj.GetComponent<Rigidbody>();
        
        if (_temporalRig != null)
        {
            _temporalRig.AddTorque(Vector3.right * -30f);
            _currentSceneObjects.Add(_temporalRig, new BodyInfo(_temporalRig.velocity, _temporalRig.angularVelocity));
        }
    }

    private void ClearFallingObjectsList()
    {
        _currentSceneObjects.Clear();
    }

    private void IncreaseDifficulty()
    {
        _difficulty++;
        if (_difficulty > _maxDifficulty)
        {
            _difficulty = _maxDifficulty;
        }
        _saveData.SaveData(_difficulty, SaveKeyManager.Difficulty);
    }
    private void ReduceDifficulty()
    {
        _difficulty -= 2;
        if (_difficulty < _minDifficulty)
        {
            _difficulty = _minDifficulty;
        }
        _saveData.SaveData(_difficulty, SaveKeyManager.Difficulty);
    }

    private void OnLevelEnd()
    {
        ClearFallingObjectsList();
        OnSlowModeEnd();
    }
}