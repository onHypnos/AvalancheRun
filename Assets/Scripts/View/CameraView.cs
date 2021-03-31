using System.Collections;
using UnityEngine;


public class CameraView : BaseObjectView
{
    [SerializeField] private GameObject _pursuedObject;
    [SerializeField] private float _stopChangingDistance = 1f;
    [SerializeField] private float _smooth = 0.9f;
    [SerializeField] private bool _objectChanging = false;
    [SerializeField] private bool _cameraRotate;

    public GameObject PursuedObject => _pursuedObject;
    public float StopChangingDistance => _stopChangingDistance;
    public float Smooth => _smooth;
    public bool ObjectChanging => _objectChanging;
    public bool IsRotate => _cameraRotate;
    
    
    public void SetPursuedObject(GameObject obj)
    {
        _pursuedObject = obj;
        _objectChanging = true;
    }

    public void SetObjectChangingState(bool value)
    {
        _objectChanging = value;
    }
}
