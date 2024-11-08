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

    public void HandleInput()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
            _rb.transform.localScale = new Vector3(moveInput * (-1), 1, 1);
    }

    public void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(moveInput * _speed, _rb.velocity.y);
    }
}


