using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash
{
    private float _dashForce;
    private float _dashDuration;
    private Rigidbody2D _rb;
    public bool isDashing;
    public int dashBuffer = 1;
    public float originalGravity;
    public ActionType actionType = ActionType.Dash;

    public Dash(float dashForce, float dashDuration, Rigidbody2D rb)
    {
        _dashForce = dashForce;
        _dashDuration = dashDuration;
        _rb = rb;
    }

    public void RequestDash()
    {
        isDashing = true;
    }

    public void DashLogic()
    {
        originalGravity = _rb.gravityScale;
        Vector2 inputDirection = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputDirection != Vector2.zero)
        {
            _rb.linearVelocity = inputDirection.normalized * _dashForce;
        }
        dashBuffer--;
    }

}
