using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Cinemachine;

public class Hero : Entity
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDuration;

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

    [Obsolete]
    private void Update()
    {
        
        _movement.HandleInput(Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StartCoroutine(JumpTimer());
            _jump.RequestJump(); 
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dash.dashBuffer > 0)
        {
            _dash.RequestDash();
            FindObjectOfType<ComboSystem>().RegisterAttack(_dash.actionType);

        }
        if(transform.position.y <= -40)
        {
            transform.position = new(0, 0, 0);
        }
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
            _movement.Move(Input.GetAxis("Horizontal"));
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
        _rb.linearVelocity = Vector2.zero;
    }
}
