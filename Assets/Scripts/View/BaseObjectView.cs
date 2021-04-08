using UnityEngine;
public class BaseObjectView : MonoBehaviour
{
    public Transform Transform => transform;
    public Vector3 Position
    { 
        get => transform.position;
        set => transform.position = value;
    }
    public Quaternion Rotation
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }
    /*
    [SerializeField] private float MaxHealth = 100;
    [SerializeField] private float Health = 100;
    */

}