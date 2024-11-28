using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jump
{
    protected float _jumpForce;
    protected Rigidbody2D _rb;
    public bool jumpRequest;
    protected int _jumpBuffer = 1;
    public bool isGrounded;
    public ActionType actionType = ActionType.Jump;

    public Jump(float jumpForce, Rigidbody2D rb)
    {
        _jumpForce = jumpForce;
        _rb = rb;
    }

    public void RequestJump()
    {
        jumpRequest = true;
    }

    public virtual void JumpLogic()
    {
        if (jumpRequest && (isGrounded || _jumpBuffer > 0))
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            jumpRequest = false;
            _jumpBuffer--;
        }
        if (isGrounded) _jumpBuffer = 1;
    }

    public void GroundCheck(BoxCollider2D collider, LayerMask groundLayer)
    {
        Vector2 boxCenter = new(collider.bounds.center.x, collider.bounds.min.y - 0.1f);
        Vector2 boxSize = new(collider.bounds.size.x * 1.2f, 0.1f);

        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, groundLayer);
        isGrounded = hits.Length > 0;
    }
    
}

