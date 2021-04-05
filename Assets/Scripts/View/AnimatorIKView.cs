using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorIKView : BaseObjectView
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayLenght;
    [Range (0, 1f)]
    [SerializeField] private float _distanceToGround;

    private Animator _animator;
    private Ray _ray;
    private RaycastHit _hit;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_animator)
        {
            //Left foot set
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _animator.GetFloat("IKLeftFootWeight"));
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _animator.GetFloat("IKLeftFootWeight"));
            //Right foot set
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _animator.GetFloat("IKRightFootWeight"));
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _animator.GetFloat("IKRightFootWeight"));

            //Left foot action
            _ray = new Ray(_animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(_ray, out _hit, _distanceToGround + 1f, _layerMask))
            {
                Vector3 footPosition = _hit.point;
                footPosition.y += _distanceToGround;
                _animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                _animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, _hit.normal));
            }

            //Right foot action
            _ray = new Ray(_animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(_ray, out _hit, _distanceToGround + 1f, _layerMask))
            {
                Vector3 footPosition = _hit.point;
                footPosition.y += _distanceToGround;
                _animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                _animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, _hit.normal));
            }
        }
    }
}