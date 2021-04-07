using System.Collections.Generic;
using System;
using UnityEngine;


public class CollectableController : BaseController, IExecute
{
    private PlayerView _player;
    private List<CollectableView> _collectables = new List<CollectableView>();

    public int Money { get; private set; }
    public CollectableController(MainController main) : base(main)
    {
        ///Здесь присваиваем money из playerprefs
        ///
        _player = main.GetController<PlayerController>().GetPlayer;
        GameEvents.current.OnAddMoney += AddMoney;
        GameEvents.current.OnRemoveMoney += RemoveMoney;
        //GameEvents.current.OnEnemyKilled += SpawnRewardForKillingEnemy;
    }

    public override void Execute()
    {
        base.Execute();
        foreach (CollectableView view in _collectables)
        {
            if (view.IsCollected)
            {
                if (!view.ColliderIsTrigger) view.SetColliderTrigger();
                view.transform.position = Vector3.MoveTowards(view.transform.position, _player.Position + Vector3.up, 0.1f);                
            }
        }
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

    private void SpawnRewardForKillingEnemy(EnemyView enemy)
    {
        for (int i = 0; i < 3; i++)
        {
            var obj = ObjectSpawner.current.CreateCoin(enemy.Position);
            AddView(obj.GetComponent<CollectableView>());
            obj.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f, UnityEngine.Random.Range(-1f, 1f)) * 2f, ForceMode.Impulse);
            obj.GetComponent<CollectableView>().SetCollected(1f);
        }
    }
}