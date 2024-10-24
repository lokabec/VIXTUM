using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 300f;
    [SerializeField] private float _dashForce = 25f; // Скорость рывка
    [SerializeField] private float DashDuration = 0.2f; // Длительность рывка
   // [SerializeField] private float DashCooldown = 1f; // Кулдаун между рывками


    private bool _isGrounded;
    private Rigidbody2D _rb;
    private bool _jumpRequest;
    private BoxCollider2D _collider;
    private int _jumpBuffer;
    private int _dashBuffer = 1;
    private bool _isDashing;
    //private bool _canDash = true;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpRequest = true;
            StartCoroutine(JumpTimer());
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dashBuffer > 0)
        {
            _isDashing = true;
        }
    }
    void FixedUpdate()
    {
        GroundCheck();
        if (_isDashing) 
        {
            DashLogic(); 
        }
        else
        {
            MovementLogic();
            JumpLogic();
        }
    }

    private void MovementLogic()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Debug.Log(moveInput);
        _rb.velocity = new Vector2(moveInput * _speed, _rb.velocity.y);
    }

    private void JumpLogic()
    {
        if ((_jumpRequest && _isGrounded) || (_jumpRequest && _jumpBuffer > 0))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _jumpRequest = false;
            _jumpBuffer--;
        }
        if (_isGrounded) _jumpBuffer = 1;
    }

    private void DashLogic()
    {
        
        float originalGravity = _rb.gravityScale;
        Vector2 inputDirection = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputDirection != Vector2.zero)
        {
            _rb.velocity = inputDirection.normalized * _dashForce;
        }
        _dashBuffer--;
        StartCoroutine(DashTimer(originalGravity));
        
    }

    private void GroundCheck()
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(new(transform.position.x + 0.1535808f, transform.position.y), new(_collider.size.x, 0.2f), 0f);
        _isGrounded = collider.Length > 1;

        if (_isGrounded) _dashBuffer = 1;
    }

    private IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(0.1f);
        _jumpRequest = false;
    }
    private IEnumerator DashTimer(float originalGravity)
    {
        yield return new WaitForSeconds(DashDuration);
        _isDashing = false;
        _rb.gravityScale = originalGravity;
        _rb.velocity = Vector2.zero;
    }

   
}
