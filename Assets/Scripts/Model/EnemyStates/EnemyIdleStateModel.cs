using UnityEngine;
public class EnemyIdleStateModel : BaseEnemyStateModel
{
    public override void Execute(EnemyView enemy, EnemyController controller)
    {
        base.Execute(enemy, controller);
        if (_magnitude > 0)
        {
            _magnitude -= 5;
        }
        if (_magnitude < 0)
        {
            _magnitude = 0;
        }
    }
}