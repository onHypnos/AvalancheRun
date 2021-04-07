using UnityEngine;
public abstract class BaseEnemyStateModel : IEnemyState
{
    private Vector3 _raycastOffset = new Vector3(0, 1, 0);
    private RaycastHit _hit;
    private float _footDistance = 1f;
    protected float _magnitude = 0f;
    public virtual void Execute(EnemyView enemy, EnemyController controller)
    {
        if (enemy.State != EnemyStates.Dead)
        {
            if (Physics.Raycast(enemy.transform.position + _raycastOffset, Vector3.down, out _hit, 5f))
            {
                enemy.transform.position += new Vector3(0, (_footDistance - _hit.distance), 0);
            }
        }        
        if (_magnitude > 100)
        {
            _magnitude = 100.0f;
        }
        enemy.Animator.SetFloat("VectorSpeedMagnitude", _magnitude * 0.01f);
    }    
}