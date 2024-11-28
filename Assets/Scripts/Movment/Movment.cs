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

        // Получаем мировые координаты мыши
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Проверяем, находится ли мышь слева или справа от персонажа
        if (mousePosition.x < _rb.transform.position.x)
        {
            _rb.transform.localScale = new Vector3(1, 1, 1); // Смотрит вправо
        }
        else if (mousePosition.x > _rb.transform.position.x)
        {
            _rb.transform.localScale = new Vector3(-1, 1, 1); // Смотрит влево
        }
    }

    public virtual void Move(float moveInput)
    {
        _rb.linearVelocity = new Vector2(moveInput * _speed, _rb.linearVelocity.y);
    }
}


