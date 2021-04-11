using System.Collections.Generic;
using System;
using UnityEngine;


public class CollectableController : BaseController, IExecute
{
    private List<CollectableView> _collectables = new List<CollectableView>();
    private SaveDataRepo _save;

    private int _bank;
    private int _money;
    private float _rotationSpeed;


    public int Bank => _bank;
    public int Money => _money;

    public CollectableController(MainController main) : base(main)
    {
        _save = new SaveDataRepo();
        _bank = _save.LoadInt(SaveKeyManager.Bank);
        _money = 0;

        GameEvents.current.OnAddMoney += AddMoney;
        GameEvents.current.OnRemoveMoney += RemoveMoney;
        GameEvents.current.OnLevelComplete += CompleteLevel;
        GameEvents.current.OnLevelStart += StartLevel;
    }

    public override void Execute()
    {
        base.Execute();

        GameEvents.current.GetCurrentMoney(_money);
        GameEvents.current.GetBank(_bank);

        // TODO
        // Rotate money
    }

    public void AddView(CollectableView view)
    {
        if (!_collectables.Contains(view))
        {
            _collectables.Add(view);
        }
        else
        {
            Debug.Log($"Object {view} was already added to update list");
        }
    }

    public void RemoveView(CollectableView view)
    {
        if (_collectables.Contains(view))
        {
            _collectables.Remove(view);
        }
        else
        {
            Debug.Log($"Object {view} was already removed from update list");
        }
    }

    private void AddMoney(int value)
    {
        _money += value;
    }

    private void RemoveMoney(int value)
    {
        _bank -= value;
    }

    private void StartLevel()
    {
        _money = 0;
    }

    private void CompleteLevel()
    {
        _bank += _money;
        _save.SaveData(_bank, SaveKeyManager.Bank);
    }
}