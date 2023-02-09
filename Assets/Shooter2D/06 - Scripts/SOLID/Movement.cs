using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputsReceiver _inputsReceiver;
    float _moveDirection;

    public float MoveDirection
    {
        get { return _moveDirection; }
        set { _moveDirection = value; }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _moveDirection = _inputsReceiver.Horizontal;
    }
}