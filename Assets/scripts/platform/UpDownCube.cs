using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    horizontal,
    vertical
}

public class UpDownCube : MonoBehaviour
{
    Vector3 _startPos;
    Vector3 _endPos;
    sbyte direction = 1;
    
    public MoveDirection _moveDirection;
    public float speed = 2.1f;
    public float path = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;

        switch (_moveDirection)
        {
            case MoveDirection.horizontal:
                _endPos = _startPos + new Vector3(path, 0, 0);
                break;
            case MoveDirection.vertical:
                _endPos = _startPos + new Vector3(0, path, 0);
                break;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction == 1)
        {
            //move up
            if (_moveDirection == MoveDirection.vertical)
            {
                if (transform.position.y < _endPos.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + direction * speed * Time.fixedDeltaTime, transform.position.z);
                    if (Mathf.Abs(transform.position.y - _endPos.y) <= speed * Time.fixedDeltaTime)
                    {
                        transform.position = _endPos;
                    }
                }
                else
                {
                    direction = -1;
                    _startPos = _endPos;
                    _endPos = new Vector3(_endPos.x, _startPos.y + direction * path, _endPos.z);
                }
            }

            //move right
            if (_moveDirection == MoveDirection.horizontal)
            {
                if (transform.position.x < _endPos.x)
                {
                    transform.position = new Vector3(transform.position.x + direction * speed * Time.fixedDeltaTime, transform.position.y , transform.position.z);
                    if (Mathf.Abs(transform.position.x - _endPos.x) <= speed * Time.fixedDeltaTime)
                    {
                        transform.position = _endPos;
                    }
                }
                else
                {
                    direction = -1;
                    _startPos = _endPos;
                    _endPos = new Vector3(_endPos.x + direction * path, _startPos.y , _endPos.z);
                }
            }
        }
        else
        {
            if (_moveDirection == MoveDirection.vertical)
            {
                if (transform.position.y > _endPos.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + direction * speed * Time.fixedDeltaTime, transform.position.z);
                    if (Mathf.Abs(transform.position.y - _endPos.y) <= speed * Time.fixedDeltaTime)
                    {
                        transform.position = _endPos;
                    }
                }
                else
                {
                    direction = 1;
                    _startPos = _endPos;
                    _endPos = new Vector3(_endPos.x, _startPos.y + direction * path, _endPos.z);
                }
            }

            //move left
            if (_moveDirection == MoveDirection.horizontal)
            {
                if (transform.position.x > _endPos.x)
                {
                    transform.position = new Vector3(transform.position.x + direction * speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
                    if (Mathf.Abs(transform.position.x - _endPos.x) <= speed * Time.fixedDeltaTime)
                    {
                        transform.position = _endPos;
                    }
                }
                else
                {
                    direction = -1;
                    _startPos = _endPos;
                    _endPos = new Vector3(_endPos.x + direction * path, _startPos.y, _endPos.z);
                }
            }
        }
    }
}
