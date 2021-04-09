using System.Collections;
using UnityEngine;

public static class Utilits
{
    private static Vector3 _temp;

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
    public static IEnumerator CreatingObjects(GameObject[] objects, Transform transform, float deltaTime)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            _temp = transform.position + Vector3.right * (i % 4)* 1.6f * (Mathf.Pow(-1, i));
            GameEvents.current.CreatingNewFallingItem(GameObject.Instantiate(objects[i], _temp, Quaternion.identity, transform));
            yield return new WaitForSecondsRealtime(deltaTime);
        }
    }

    public static IEnumerator BombShieldBehavior(GameObject bombShield)
    {
        for (int i = 0; i < 500; i++)
        {
            bombShield.transform.localScale = Vector3.one * i * .1f;
            yield return null;
        }
        bombShield.SetActive(false);
    }
}