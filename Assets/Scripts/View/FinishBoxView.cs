using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBoxView : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.current.LevelEnd();
        }
    }
}