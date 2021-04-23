using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    public void OnPlayerAttackStart()
    {
        GameEvents.Current.PlayerAttackStart();
    }

    public void OnPlayerAttackEnd()
    {
        GameEvents.Current.PlayerAttackEnd();
    }

}
