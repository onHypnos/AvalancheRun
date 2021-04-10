using System.Collections;
using UnityEngine;


public class BodyInfo
{
    public Vector3 UnscaledVelocity;
    public Vector3 UnscaledAngularVelocity;
    public Vector3? PrevVelocity;
    public Vector3? PrevAngularVelocity;

    public BodyInfo(Vector3 rigVelocity, Vector3 rigAngularVelocity)
    {
        PrevVelocity = null;
        UnscaledVelocity = rigVelocity;
        UnscaledAngularVelocity = rigAngularVelocity;
    }
}
