using UnityEngine;
public abstract class BaseEnemyStateModel : IEnemyState
{    
    public virtual void Execute(EnemyView enemy, EnemyController controller)
    {
        if (enemy.State != EnemyStates.Dead)
        {
            enemy.RotateVisualTrigger();
            if (controller.PlayerInFieldOfView(enemy, controller.Player))
            {
                enemy.EnableAllertTrigger();
            }
            else
            {
                enemy.DisableAllertTrigger();
            }
        }
        
    }    
}