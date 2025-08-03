using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraMoveSpeed;
    private Transform _target;
    internal void SetTarget(Transform target)
    {
        _target = target;
        transform.position = new Vector3()
        {
            x = _target.position.x,
            y = _target.position.y,
            z = _target.position.z - 10
        };
    }

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_target)
        {
            Vector3 target = new Vector3()
            {
                x = _target.position.x,
                y = _target.position.y,
                z = _target.position.z - 10
            };

           Vector3 lerpPos = Vector3.Lerp(transform.position, target, _cameraMoveSpeed * Time.deltaTime);
            
            transform.position = lerpPos;
        }
    }
}
