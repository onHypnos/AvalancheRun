using System.Collections.Generic;
using System;
using UnityEngine;


public class CollectableController : BaseController, IExecute
{
    private List<CollectableView> _collectables = new List<CollectableView>();
    private SaveDataRepo _save;

    private int _bank;
    private int _money;


    public int Bank => _bank;
    public int Money => _money;

    public CollectableController(MainController main) : base(main)
    {
        _save = new SaveDataRepo();
        _bank = _save.LoadInt(SaveKeyManager.Bank);

        GameEvents.current.OnAddMoney += AddMoney;
        GameEvents.current.OnRemoveMoney += RemoveMoney;
    }

    public override void Execute()
    {
        base.Execute();
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

    }

    private void RemoveMoney(int value)
    {

    }
}