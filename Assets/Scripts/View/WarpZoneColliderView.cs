using System.Collections;
using UnityEngine;

public class WarpZoneColliderView : BaseObjectView
{
    private float _warpObstacleDelta = 0;
    public float WarpOstacleDelta => _warpObstacleDelta;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            _warpObstacleDelta = this.transform.localScale.z;
            InputEvents.current.TriggerWarpWall(WarpOstacleDelta);
        }        
    }
}
