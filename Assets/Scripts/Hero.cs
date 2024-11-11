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
            FindObjectOfType<ComboSystem>().RegisterAttack(_dash.actionType);

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
}
