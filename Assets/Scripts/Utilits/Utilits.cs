using System.Collections;
using UnityEngine;

public static class Utilits
{    
    public static IEnumerator MoveToTarget(Transform obj, Vector3 target)
    {        
        if (obj == null) yield break;

        while (obj.position != target)
        {
            obj.position = Vector3.MoveTowards(obj.position, target, Time.deltaTime);
            yield return null;
        }
    }
    /*
    public static IEnumerator CustomInvoke(float time, float deltaTime)
    { 
        while (Time.time != time+deltaTime)
        {
            yield return null;
        }
        yield break;
    }
    */
}