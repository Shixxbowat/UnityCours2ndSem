using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputsReceiver : MonoBehaviour
{
    [SerializeField] Movement playerMovement;
    // CONTROLLER
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] Vector2 _directionRotation;

    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private PlayerControls.PlayerActions _playerControlsActions;

    // ACCESSEURS
    private float _horizontal;
    public float Horizontal => _horizontal;

    private float _vertical;
    public float Vertical => _vertical;

    // VARS
    private float _speed = 8f;

    private float _jumpingPower = 8f;

    private bool _isFacingRight = true;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask("Ground");

        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Move.performed += SetHorizontal;
        _playerControls.Player.Move.canceled += SetHorizontal;
    }

    private void SetHorizontal(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }
    private void SetVertical(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Player.Move.performed -= SetHorizontal;
        _playerControls.Player.Move.canceled -= SetHorizontal;
    }

    void Update()
    {
        VelocityApplier();
    }

    public void VelocityApplier()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
    }
}