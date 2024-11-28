using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (moveInput > 0)
            _rb.transform.localScale = new Vector3(Mathf.Ceil(moveInput) * (-1), 1, 1);
        if(moveInput < 0)
            _rb.transform.localScale = new Vector3(Mathf.Floor(moveInput) * (-1), 1, 1);
    }

    public virtual void Move(float moveInput)
    {
        _rb.linearVelocity = new Vector2(moveInput * _speed, _rb.linearVelocity.y);
    }
}


