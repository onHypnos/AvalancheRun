using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCameraView : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private int _basePriority;
    public void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _basePriority = _camera.Priority;
        GameEvents.current.OnSettingActiveCamera += SetActiveCamera;
    }
    
    public void SetActiveCamera(string name)
    {
        if (this.gameObject.name == name)
        {
            _camera.Priority = 100;
        }
        else 
        {
            _camera.Priority = _basePriority;
        }
    }
    

    private void OnDestroy()
    {
        GameEvents.current.OnSettingActiveCamera -= SetActiveCamera;
    }
}
