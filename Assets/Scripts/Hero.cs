using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 300f;
    [SerializeField] private float _dashForce = 25f;
    [SerializeField] private float _dashDuration = 0.2f;

    private Rigidbody2D _rb;
    private Movement _movement;
    private Jump _jump;
    private Dash _dash;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movement = new Movement(_speed, _rb);
        _jump = new Jump(_jumpForce, _rb);
        _dash = new Dash(_dashForce, _dashDuration , _rb);
    }

    private void Update()
    {
        _movement.HandleInput();
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StartCoroutine(JumpTimer());
            _jump.RequestJump(); 
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dash.dashBuffer > 0)
        {
            _dash.RequestDash();
            
        }
        Debug.Log($"Буфер: {_dash.dashBuffer}, Гравитация: {_rb.gravityScale}, Состояние рывка: {_dash.isDashing}");
    }

    private void FixedUpdate()
    {
        _jump.GroundCheck(GetComponent<BoxCollider2D>(), LayerMask.GetMask("Ground"));
        if (_jump.isGrounded) _dash.dashBuffer = 1;
        if (_dash.isDashing)
        {
            _dash.DashLogic();
            StartCoroutine(DashTimer(_dash.originalGravity));
        }
        else
        {
            _movement.Move();
            _jump.JumpLogic();
        }
        
    }

    private IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(0.1f);
       _jump.jumpRequest = false;
    }

    private IEnumerator DashTimer(float originalGravity)
    {
        yield return new WaitForSeconds(_dashDuration);
        _dash.isDashing = false;
        _rb.gravityScale = originalGravity;
        _rb.velocity = Vector2.zero;
    }

    //[SerializeField] private float _speed = 10f;
    //[SerializeField] private float _jumpForce = 300f;
    //[SerializeField] private float _dashForce = 25f; // Скорость рывка
    //[SerializeField] private float _dashDuration = 0.2f; // Длительность рывка
    //[SerializeField] private CinemachineVirtualCamera _camera;


    //public bool _isGrounded;
    //private Rigidbody2D _rb;
    //private bool _jumpRequest;
    //private BoxCollider2D _collider;
    //private int _jumpBuffer;
    //private int _dashBuffer = 1;
    //[HideInInspector] public bool _isDashing;


    //void Start()
    //{
    //    _rb = GetComponent<Rigidbody2D>();
    //    _collider = GetComponent<BoxCollider2D>();
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        _jumpRequest = true;
    //        StartCoroutine(JumpTimer());
    //    }
    //    if (Input.GetKeyDown(KeyCode.LeftShift) && _dashBuffer > 0)
    //    {
    //        _isDashing = true;
    //    }
    //}
    //void FixedUpdate()
    //{
    //    GroundCheck();
    //    if (_isDashing) 
    //    {
    //        DashLogic(); 
    //    }
    //    else
    //    {
    //        MovementLogic();
    //        JumpLogic();
    //    }
    //}

    //private void MovementLogic()
    //{
    //    float moveInput = Input.GetAxisRaw("Horizontal");
    //    _rb.velocity = new Vector2(moveInput * _speed, _rb.velocity.y);
    //    if(moveInput != 0) transform.localScale = new(moveInput * (-1),1);
    //}

    //private void JumpLogic()
    //{
    //    if ((_jumpRequest && _isGrounded) || (_jumpRequest && _jumpBuffer > 0))
    //    {
    //        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    //        _jumpRequest = false;
    //        _jumpBuffer--;
    //    }
    //    if (_isGrounded) _jumpBuffer = 1;
    //}

    //private void DashLogic()
    //{

    //    float originalGravity = _rb.gravityScale;
    //    Vector2 inputDirection = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //    if (inputDirection != Vector2.zero)
    //    {
    //        _rb.velocity = inputDirection.normalized * _dashForce;
    //    }
    //    _dashBuffer--;
    //    StartCoroutine(DashTimer(originalGravity));

    //}

    //private void GroundCheck()
    //{
    //    Collider2D[] collider = Physics2D.OverlapBoxAll(new(transform.position.x + 0.1535808f, transform.position.y), new(_collider.size.x, 0.2f), 0f);
    //    _isGrounded = collider.Length > 1;

    //    if (_isGrounded) _dashBuffer = 1;
    //}

    //private IEnumerator JumpTimer()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    _jumpRequest = false;
    //}
    //private IEnumerator DashTimer(float originalGravity)
    //{
    //    yield return new WaitForSeconds(_dashDuration);
    //    _isDashing = false;
    //    _rb.gravityScale = originalGravity;
    //    _rb.velocity = Vector2.zero;
    //}

    //internal IEnumerator LockupInAirWhileAttack(Attack attack)
    //{

    //    float originalGravivty = _rb.gravityScale;
    //    Vector2 originalVelocity = _rb.velocity;

    //    _rb.velocity = Vector2.zero;
    //    _rb.gravityScale = 0f;

    //    yield return new WaitForSeconds(attack.cooldown);
    //    Debug.Log("Работает");

    //    _rb.velocity = originalVelocity;
    //     _rb.gravityScale = originalGravivty;

    //}
}
