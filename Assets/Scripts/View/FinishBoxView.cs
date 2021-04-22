using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBoxView : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.Current.LevelEnd();
            GameEvents.Current.LevelComplete();
        }
        if (other.CompareTag("Member"))
        {
            GameEvents.Current.MemberFinish(other.gameObject.GetComponent<EnemyView>());
        }
    }
}
