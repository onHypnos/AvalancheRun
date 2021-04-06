using UnityEngine;
public class PlayerAttackColliderView : BaseObjectView
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameEvents.current.PlayerCollideEnemy(other);
        }
    }
}
