using System.Collections;
using UnityEngine;

public class EnemyDive
{
    private Rigidbody2D _rb;
    public Collider2D[] targets;
    private Vector2 _startPosition;
    public bool isDiving = false;
    public int diveBuffer = 1;  // Счетчик для повторных пикирований
    private float _diveSpeed;  // Скорость пикирования
    private float _returnSpeed;  // Скорость возвращения
    private float _diveDistanceThreshold;  // Порог для завершения пикирования
    private int rnd;

    public EnemyDive(Rigidbody2D rb, float diveSpeed, float returnSpeed, float diveDistanceThreshold)
    {
        _rb = rb;
        _diveSpeed = diveSpeed;
        _returnSpeed = returnSpeed;
        _diveDistanceThreshold = diveDistanceThreshold;

    }

    public void RequestDive()
    {
        if (diveBuffer > 0)
        {
            isDiving = true;
            _startPosition = _rb.position;  
            diveBuffer--;
            
            rnd = Random.Range(0, targets.Length);
        }
    }

    public void DiveLogic()
    {
        Vector2[] trgt = new Vector2[targets.Length];
        for (int i = 0; i < trgt.Length; i++)
        {
            trgt[i] = new Vector2(targets[i].GetComponent<Transform>().position.x, targets[i].GetComponent<Transform>().position.y);
        }
        Vector2 direction = trgt[rnd] - (_rb.position - new Vector2(0, _rb.GetComponent<Collider2D>().transform.localScale.y * 2));
        _rb.linearVelocity = direction * _diveSpeed;

        
    }

    public void ReturnLogic()
    {
        _rb.linearVelocity =  (new Vector2(_rb.position.x, _startPosition.y) - _rb.position)  * _returnSpeed;
    }

    
}
