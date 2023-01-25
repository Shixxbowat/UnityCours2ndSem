using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter2D : MonoBehaviour
{
    // SHOOTS
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;

    // CONTROLLER
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] Vector2 _directionRotation;
    [SerializeField] Transform _pivotRotation;
    [SerializeField] private PlayerControls _playerInputs;
    [SerializeField] private PlayerControls.PlayerActions _playerInputsTest;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // _playerInputs = new PlayerControls();
        // _playerInputsTest.Shoot.performed += ctx => Shoot();
    }

    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (!isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        _directionRotation = (context.ReadValue<Vector2>() - (Vector2)Camera.main.WorldToScreenPoint(_pivotRotation.position)).normalized;
        SetRotationToPivot();
    }

    private void SetRotationToPivot()
    {
        float angle = Mathf.Atan2(_directionRotation.y, _directionRotation.x);
        _pivotRotation.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * angle);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }    
}
