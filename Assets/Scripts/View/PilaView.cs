using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PilaView : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _speed = 5f;
    private bool _moveToEnd = true;

    public void Awake()
    {
        transform.position = _startPosition.position;

    }

    public void Update()
    {
        transform.Rotate(0, 0, 15);
        if (_moveToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition.position, Time.deltaTime * _speed);
            if (transform.position == _endPosition.position)
            {
                _moveToEnd = false;
            }
        }
        if (!_moveToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition.position, Time.deltaTime * _speed);
            if (transform.position == _startPosition.position)
            {
                _moveToEnd = true;
            }
        }
        
    }
}
