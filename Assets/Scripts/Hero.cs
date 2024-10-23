using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 300f;
    [SerializeField] private float DashSpeed = 25f; // Скорость рывка
    [SerializeField] private float DashDuration = 0.2f; // Длительность рывка
    [SerializeField] private float DashCooldown = 1f; // Кулдаун между рывками


    private bool _isGrounded;
    private Rigidbody2D _rb;
    private bool _jumpRequest;
    private BoxCollider2D _collider;
    private int _jumpBuffer;
    private bool _isDashing = false;
    private bool _canDash = true;


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
        if (Input.GetKeyDown(KeyCode.LeftShift) && _isGrounded && !_isDashing)
        {
            StartCoroutine(Dash());
        }
    }
    void FixedUpdate()
    {
        GroundCheck();
        if (!_isDashing)
        {
            MovementLogic();
            JumpLogic();
        }
        
    }

    private void MovementLogic()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");  
        _rb.velocity = new Vector2(moveInput * _speed, _rb.velocity.y);


    }

    private void JumpLogic()
    {
        if ((_jumpRequest && _isGrounded) || (_jumpRequest && _jumpBuffer != 0))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _jumpRequest = false;
            _jumpBuffer--;
        }
        if (_isGrounded == true)
        {
            _jumpBuffer = 1;
        }
    }

    private void GroundCheck()
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(new(transform.position.x + 0.1535808f, transform.position.y), new(_collider.size.x, 0.2f), 0f);
        _isGrounded = collider.Length > 1;
    }

    private IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(0.1f);
        _jumpRequest = false;
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        _isGrounded = false;

        float originalGravity = _rb.gravityScale; // Сохраняем оригинальную гравитацию
        _rb.gravityScale = 0f; // Отключаем гравитацию на время рывка


        // Выполняем рывок в сторону движения персонажа
        _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * DashSpeed, Input.GetAxisRaw("Vertical") * DashSpeed * 0.7f);
        

        // Ждем завершения рывка
        yield return new WaitForSeconds(DashDuration);

        _rb.gravityScale = originalGravity; // Возвращаем гравитацию
        _isDashing = false;

        // Ждем кулдаун перед возможностью следующего рывка
        yield return new WaitForSeconds(DashCooldown);
        _isGrounded = true;
    }
}
