using UnityEngine;

public class LeftLegAttackCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameEvents.Current.OnEnemyGetDamage(other.GetComponent<EnemyView>());
        }
    }
}
