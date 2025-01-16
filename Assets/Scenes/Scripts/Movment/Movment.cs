using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movement
{
    private float _speed;
    private Rigidbody2D _rb;

    public Movement(float speed, Rigidbody2D rb)
    {
        _speed = speed;
        _rb = rb;
    }

    public virtual void HandleInput(float moveInput)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < _rb.transform.position.x)
        {
            _rb.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (mousePosition.x > _rb.transform.position.x)
        {
            _rb.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public virtual void Move(float moveInput)
    {
        _rb.linearVelocity = new Vector2(moveInput * _speed, _rb.linearVelocity.y);
    }
}


