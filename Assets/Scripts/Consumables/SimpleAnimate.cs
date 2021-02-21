using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotateOnAxis { x, y, z};
public class SimpleAnimate : MonoBehaviour
{
    [SerializeField] RotateOnAxis _rotationAxis = RotateOnAxis.x;

    [SerializeField] bool _bobAnimate = true;
    [SerializeField] bool _rotate = true;

    [SerializeField] float _rotationSpeed = 75;
    [SerializeField] float _bobSpeed = 0.45f;
    [SerializeField] float _bobHeight = 1f;

    private Vector3 _startPos;
    private Vector3 _targetPos;
    private bool _toTarget = true;

    private void Start()
    {
        _startPos = transform.position;
        _targetPos = new Vector3(_startPos.x, _startPos.y + _bobHeight, _startPos.z);
    }

    void Update()
    {
        if(_bobAnimate)
        {
            //Lerp to target position
            if (_toTarget) 
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, _bobSpeed * Time.deltaTime);
                if (transform.position == _targetPos)
                    _toTarget = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPos, _bobSpeed * Time.deltaTime);
                if (transform.position == _startPos)
                    _toTarget = true;
            }
        }

        if(_rotate)
        {
            if(_rotationAxis == RotateOnAxis.x)
                transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
            if (_rotationAxis == RotateOnAxis.y)
                transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
            if (_rotationAxis == RotateOnAxis.z)
                transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }
    }
}
