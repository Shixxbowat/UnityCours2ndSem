using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter2D : MonoBehaviour
{
    // CONTROLLER
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] Vector2 _directionRotation;
   
    [SerializeField] private PlayerControls _playerInputs;
    [SerializeField] private PlayerControls.PlayerActions _playerInputsTest;


    private float _horizontal;
    private float _speed = 8f;
    private float _jumpingPower = 8f;


    private bool _isFacingRight = true;
   
    private WeaponController _weaponController;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _weaponController = GetComponent<WeaponController>();
        
        // _playerInputs = new PlayerControls();
        // _playerInputsTest.Shoot.performed += ctx => Shoot();
    }

    private void OnEnable()
    {
        _playerInputs = new PlayerControls();

        _playerInputs.Enable();
        _playerInputs.Player.Shoot.performed += ctx => Shooting();
        _playerInputs.Player.Shoot.canceled += ctx => CanceledShooting();
    }
    private void OnDisable()
    {
        _playerInputs.Disable();
        _playerInputs.Player.Shoot.performed -= ctx => Shooting();
        _playerInputs.Player.Shoot.canceled -= ctx => CanceledShooting();
    }

    void Shooting()
    {
        _weaponController.Shooting = true;
    }

    void CanceledShooting()
    {
        _weaponController.Shooting = false;
    }

    void Update()
    {
        Shoot();
        VelocityApplier();
        // FacingRight(); // no need
    }

    public void FacingRight()
    {
        if (!_isFacingRight && _horizontal > 0f)
        {
            Flip();
        }
        else if (!_isFacingRight && _horizontal < 0f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void VelocityApplier()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        _directionRotation = (context.ReadValue<Vector2>() - (Vector2)Camera.main.WorldToScreenPoint(_weaponController.pivotRotation.position)).normalized;
        SetRotationToPivot();
    }

    private void SetRotationToPivot()
    {
        float angle = Mathf.Atan2(_directionRotation.y, _directionRotation.x);
        _weaponController.SetPivotRotation(new Vector3(0, 0, Mathf.Rad2Deg * angle));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpingPower);
        }
        if (context.canceled && _rigidbody2D.velocity.y > 0f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);
        }

        /*if (IsGrounded())
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    break;
                case InputActionPhase.Canceled:
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                    break;
            }
        }*/
    }

    public void Shoot()
    {
        _weaponController.Shoot();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }
}
